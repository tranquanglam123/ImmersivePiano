using System;
using UnityEngine;

namespace ImmersivePiano
{
    public class HeadFollowHandler : MonoBehaviour
    {
        //Reference for the camera
        [SerializeField] Vector3 Offset;
        [SerializeField] float DelayTime;
        [SerializeField] Camera CameraRig;
        private readonly string CameraRef = "CenterEyeAnchor";
        private Transform cam_trans;


        private void Start()
        {
            try
            {
                cam_trans = CameraRig.transform;
            }
            catch (NullReferenceException e)
            {
                Debug.LogException(e, this);
                CameraRig = GameObject.Find(CameraRef).GetComponent<Camera>();
            }

            if (DelayTime == 0)
            {
                DelayTime = 0.5f;
            }
        }

        private void OnEnable()
        {
            try
            {
                cam_trans = CameraRig.transform;
            }
            catch (NullReferenceException e)
            {
                Debug.LogException(e, this);
                CameraRig = GameObject.Find(CameraRef).GetComponent<Camera>();

            }
            Vector3 targetPos = cam_trans.TransformPoint(Offset);
            Quaternion targetRot = Quaternion.Euler(new Vector3(0, cam_trans.eulerAngles.y, 0));

            transform.position = targetPos;
            transform.rotation = targetRot;
        }
        private void FixedUpdate()
        {
            Vector3 targetPos = cam_trans.TransformPoint(Offset);
            Quaternion targetRot = Quaternion.Euler(new Vector3(0, cam_trans.eulerAngles.y, 0));

            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * DelayTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * DelayTime);
        }

    }
}
