using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static OVRHand;

public class InitCollider : MonoBehaviour
{
    private OVRHand hand;
    private bool isFingerPinching;
    private float pinchStrength;
    private TrackingConfidence confidence;
    private bool pointerPoseValid;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<OVRHand>();
        isFingerPinching = hand.GetFingerIsPinching(HandFinger.Index);
        pinchStrength = hand.GetFingerPinchStrength(HandFinger.Index);
        confidence = hand.GetFingerConfidence(HandFinger.Index);
        pointerPoseValid = hand.IsPointerPoseValid;
    }

    // Update is called once per frame
    private void Update()
    {
        if (pointerPoseValid)
        {
            getCollider();
        }
    }
    void getCollider()
    {
        //get the point of the index finger
        Vector3 indexFingerPoint1 = hand.PointerPose.position;
        while (indexFingerPoint1 != null)
        {
            break;
        }
        Vector3 indexFingerPoint2 = hand.PointerPose.position;
        //draw a red regtanle from the 2 points
        Debug.DrawLine(indexFingerPoint1, indexFingerPoint2, Color.red);
        return;
    }
}
