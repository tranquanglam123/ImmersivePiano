//using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;
using Utilities.XR;
public class DrawPlane : MonoBehaviour
{
    //[SerializeField] private GameObject plane;
    //[SerializeField] private GameObject planePreview;
    //[SerializeField] private LayerMask meshLayerMask;
    
    //[SerializeField] private Transform leftHand;
    //[SerializeField] private Transform rightHand;

    //[SerializeField] private bool debugDraw = true;

    //private OVRInput.Controller _activeController = OVRInput.Controller.RTouch;


   

    //private bool isPlaced;


    //private (Vector3 point, Vector3 normal, bool hit) _leftHandHit;
    //private (Vector3 point, Vector3 normal, bool hit) _rightHandHit;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    planePreview = Instantiate(planePreview, transform);
    //    plane = Instantiate(plane, transform);
    //    plane.SetActive(false);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    var togglePlacement = false;
    //    const OVRInput.Button buttonMask = OVRInput.Button.PrimaryIndexTrigger | OVRInput.Button.PrimaryHandTrigger;
    
    //    if(OVRInput.GetDown(buttonMask, OVRInput.Controller.LTouch))
    //    {
    //        _activeController = OVRInput.Controller.LTouch;
    //        togglePlacement = true;
    //    }
    //    else if(OVRInput.GetDown(buttonMask, OVRInput.Controller.RTouch))
    //    {
    //        _activeController = OVRInput.Controller.RTouch;
    //        togglePlacement = true;
    //    }

    //    var leftRay = new Ray(leftHand.position, leftHand.forward);
    //    var rightRay = new Ray(rightHand.position, rightHand.forward);

    //    var leftRaySuccess = Physics.Raycast(leftRay, out var leftHit, 100f, meshLayerMask);
    //    var rightRaySuccess = Physics.Raycast(rightRay, out var rightHit, 100f, meshLayerMask);

    //    _leftHandHit = (leftHit.point, leftHit.normal, leftRaySuccess);
    //    _rightHandHit = (rightHit.point, rightHit.normal, rightRaySuccess);

    //    var active = _activeController == OVRInput.Controller.LTouch ? _leftHandHit : _rightHandHit;

    //    if (togglePlacement && active.hit) TogglePlacement(active.point, active.normal);
    //    if (isPlaced && active.hit)
    //    {
    //        var planeTransform = plane.transform;
    //        planeTransform.position = active.point;
    //        planeTransform.up = active.normal;
    //    }
    
    //}

    //private void TogglePlacement(Vector3 point, Vector3 normal)
    //{
    //    //if plane has been set
    //    if (isPlaced)
    //    {
    //        plane.SetActive(false);
    //        planePreview.SetActive(true);
    //        isPlaced = false;
    //    }
    //    else { 
    //        var planeTransform = plane.transform; 
    //        planeTransform.position = point;
    //        planeTransform.up = normal;

    //        planePreview.SetActive(false);
    //        plane.SetActive(true);
    //        isPlaced = true;    
    //    }
    //}

    //private void DebugDraw()
    //{
    //    Color GetPointerColor(float angle)
    //    {
    //        //return the pointer color yellow
    //        if(angle > 30 && angle  < 120) return Color.yellow;
    //        if (angle >=120) return Color.red;
    //        return Color.cyan;
    //    }
    //    //If the pointer of left hand HIT
    //    if (_leftHandHit.hit)
    //    {
    //        var position = _leftHandHit.point;
    //        var rotation = Quaternion.FromToRotation(Vector3.up, _leftHandHit.normal);

    //        XRGizmos.DrawRay(position, rotation * Vector3.forward, Color.cyan);

    //        var angle = Vector3.Angle(Vector3.up, _leftHandHit.normal);
    //        var pointerColor = GetPointerColor(angle);
    //        XRGizmos.DrawPointer(position, _leftHandHit.normal, pointerColor);
    //    }
    //    if (_rightHandHit.hit)
    //    {
    //        var position = _rightHandHit.point;
    //        var rotation = Quaternion.FromToRotation(Vector3.up, _rightHandHit.normal);

    //        XRGizmos.DrawRay(position, rotation * Vector3.forward, Color.cyan);

    //        var angle = Vector3.Angle(Vector3.up, _rightHandHit.normal);
    //        var pointerColor = GetPointerColor(angle);
    //        XRGizmos.DrawPointer(position, _rightHandHit.normal, pointerColor);
    //    }
    //    return;
    //}

}
