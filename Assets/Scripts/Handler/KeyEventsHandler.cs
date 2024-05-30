using ImmersivePiano;
using Oculus.Interaction;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;

/// <summary>
/// Handles every interactions with the Piano Key
/// </summary>
public class KeyEventsHandler : MonoBehaviour
{
    #region Configure Reference
    [Tooltip("The IInteractableView (Interactable) component to wrap.")]
    [SerializeField, Interface(typeof(IInteractableView))]
    private UnityEngine.Object _interactableView;
    private IInteractableView InteractableView;

    [Tooltip("The IPressable (Key) component to wrap.")]
    [SerializeField, Interface(typeof(IPressable))]
    private UnityEngine.Object _ipressable;
    private IPressable Ipressable;

    [Tooltip("Raised when an Interactor selects the Interactable.")]
    [SerializeField]
    private UnityEvent _whenSelect;

    [Tooltip("Raised when an Interactor unselects the Interactable.")]
    [SerializeField]
    private UnityEvent _whenUnselect;

    [Tooltip("Raised eachtime an Interactor selects the Interactable even if it is being selected by a different Interactor.")]
    [SerializeField]
    private UnityEvent _whenSelectingInteractorViewAdded;

    [Tooltip("Raised eachtime an Interactor stop selecting the Interactable even if it is being selected by a different Interactor.")]
    [SerializeField]
    private UnityEvent _whenSelectingInteractorViewRemoved;

    [Tooltip("Keep raising while being interacted.")]
    [SerializeField]
    private float _pressedTime;
    private bool _pressed = false;
    #endregion

    #region Properties
    public UnityEvent WhenSelect => _whenSelect;
    public UnityEvent WhenUnselect => _whenUnselect;
    public UnityEvent WhenSelectingInteractorViewAdded => _whenSelectingInteractorViewAdded;
    public UnityEvent WhenSelectingInteractorViewRemoved => _whenSelectingInteractorViewRemoved;
    #endregion
    public bool _started = false;

    protected virtual void Awake()
    {
        //Get PokeInteractable
        try { _interactableView = transform.GetChild(0).gameObject; }
        catch (NullReferenceException)
        {
            Debug.Log("NullReferenceException caught from assigning the _interactableView");
        }

        InteractableView = _interactableView.GetComponent<IInteractableView>();
        if (InteractableView == null)
        {
            Debug.LogError("Missing IInteractableView on child GameObject");
        }

        //Get Key To Wrap
        try { _ipressable = gameObject; }
        catch (NullReferenceException)
        {
            Debug.Log("NullReferenceExeption caught from assignin the _ipressable");
        }
        Ipressable = _ipressable.GetComponent<IPressable>();
        if (Ipressable == null)
        {
            Debug.LogError("Missing Ipressable on gameObject");
        }

    }
    protected virtual void Start()
    {
        this.BeginStart(ref _started); //turn start into false so OnEnable and OnDisable methods can be skipped
        this.AssertField(InteractableView, nameof(InteractableView));
        this.AssertField(Ipressable, nameof(Ipressable));
        this.EndStart(ref _started);
        SetEvent();
    }

    protected virtual void OnEnable()
    {
        if (_started)
        {
            InteractableView.WhenStateChanged += HandleStateChanged;
            InteractableView.WhenInteractorViewAdded += HandleWhenInteractorViewAdded;
            InteractableView.WhenInteractorViewRemoved += HandleWhenInteractorViewRemoved;
        }
    }
    protected virtual void OnDisable()
    {
        if (_started)
        {
            InteractableView.WhenStateChanged -= HandleStateChanged;
        }
    }

    // Auto adding events through code can run normally, it just do not show directly in Inspector
    #region EventsConfig
    private void SetEvent()
    {
        if (_started)
        {
            WhenSelect.AddListener(KeyPressEvent);
            WhenUnselect.AddListener(KeyStopPressEvent);
        }
    }

    public void KeyPressEvent()
    {

        if (Ipressable != null)
        {
            Ipressable.Press();
        }

    }

    public void KeyStopPressEvent()
    {

        if (Ipressable != null)
        {
            Ipressable.StopPressing();
        }

    }

    public IEnumerator CheckEvent(UnityEvent test)
    {
        if (_started)
        {
            switch (test.IsUnityNull())
            {
                case true:
                    Debug.Log($"UnityEvent {test.ToString()} is null");
                    break;
                case false:
                    Debug.Log($"UnityEvent {test.ToString()} is NOT null");
                    break;
            }
        }
        yield return null;
    }
    #endregion

    #region Handler
    private void HandleStateChanged(InteractableStateChangeArgs args)
    {
        switch (args.NewState)
        {
            case InteractableState.Normal:
                break;
            case InteractableState.Hover:
                if (args.PreviousState == InteractableState.Select)
                {
                    _whenUnselect.Invoke();
                }
                break;
            case InteractableState.Select:
                if (args.PreviousState == InteractableState.Hover)
                {
                    _whenSelect.Invoke();
                }
                break;
        }
    }

    private void HandleWhenInteractorViewAdded(IInteractorView view)
    {
        WhenSelectingInteractorViewAdded.Invoke();
    }
    private void HandleWhenInteractorViewRemoved(IInteractorView view)
    {
        WhenSelectingInteractorViewRemoved.Invoke();
    }
    #endregion

    #region Inject
    public void InjectKeyEventsHandler(IInteractableView interactableView)
    {
        _interactableView = interactableView as UnityEngine.Object;
        InteractableView = interactableView;
    }
    public void InjectAllKeyEventsHandler(IInteractableView interactableView)
    {
        InjectKeyEventsHandler(interactableView);
    }
    #endregion
}
