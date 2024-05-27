using System;
using UnityEngine;
using UnityEngine.UI;

public class SongButtonHandler : MonoBehaviour
{
    [SerializeField] string _midiFileName;

    private Toggle _toggle;

    void Start()
    {
        //Fetch the Toggle GameObject
        _toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        _toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_toggle);
        });
    }

    private void ToggleValueChanged(Toggle toggle)
    {
        throw new NotImplementedException();
    }

    public string MIDIFileName
    {
        get { return _midiFileName; }
        set { _midiFileName = value; }
    }


}
