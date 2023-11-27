using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public GameObject camera;
    private bool canRecognize;
    public float waitTime = 1f;

    public Transform leftController;
    public Transform rightController;
    public Transform OVRCameraRig;
    public Transform centerPoint;

    public float offSetY = 0.2f;
    public float offSetZ = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        canRecognize = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canRecognize && GestureDetector.leftHandGesture == "Left" && GestureDetector.rightHandGesture =="Right")
        {
            ResetCameraPosition();
            canRecognize = false;
            StartCoroutine((Wait()));
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        canRecognize = true;
    }

    void ResetCameraPosition()
    {
        Vector3 controllersCenter = (leftController.position + rightController.position) / 2;
        Vector3 offset = OVRCameraRig.position - controllersCenter;
        OVRCameraRig.position = centerPoint.position + offset + new Vector3(0, offSetY, - offSetZ);
    }
}
