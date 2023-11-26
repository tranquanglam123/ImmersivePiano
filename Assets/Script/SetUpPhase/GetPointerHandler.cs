using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetPointerHandler : MonoBehaviour
{
    public OVRHand leftHand;
    public OVRHand rightHand;
    private OVRSkeleton skeleton; //skeleton used for retrieving bone positions and rotation data
    private bool isIndexFingerPinching;
    private Transform handIndexTipTransform; //transform of the left hand index
    private int count;
    public int max;
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    
    public Vector3 GetPointer(int index)
    {
        switch (index)
        {
            case 0:
                if (leftHand.IsTracked)
                {
                    skeleton = leftHand.GetComponent<OVRSkeleton>();
                    foreach (var b in skeleton.Bones)
                    {
                        if (b.Id == OVRSkeleton.BoneId.Hand_IndexTip)
                        {
                            handIndexTipTransform = b.Transform;
                        }
                    }
                    break;

                    
                }
                break;
            case 1:
                if (rightHand.IsTracked)
                {
                    skeleton = leftHand.GetComponent<OVRSkeleton>();
                    foreach (var b in skeleton.Bones)
                    {
                        if (b.Id == OVRSkeleton.BoneId.Hand_IndexTip)
                        {
                            handIndexTipTransform = b.Transform;
                        }
                    }
                    break;

                 
                }
                break;
        }
        return handIndexTipTransform.position;
    }
    public void ConfirmGetPointer()
    {
        switch (count)
        {
            case 0:
                Vector3 p0 = GetPointer(count);
                count++;
                Debug.Log("First Pointer: " + p0);
                break;
            case 1:
                Vector3 p1 = GetPointer(count);
                count = 0;
                Debug.Log("Second Pointer: " + p1);
                //OffCheckPointer();
                break;
        }
    }
    void OffCheckPointer()
    {
        gameObject.SetActive(false);
    }
}
