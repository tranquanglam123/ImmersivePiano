using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ImmersivePiano
{
    public class SplashHandler : MonoBehaviour
    {
        [SerializeField] GameObject SplashDIalog;

        private bool _startSplash;
        private bool _inProgress;
        private bool _handTracked;


        private void Start()
        {
            _startSplash = false;
            _inProgress = false;
            SplashDIalog.SetActive(false);
        }

        private void Update()
        {
            //get the active state of the OVRCameraRig
            _startSplash = SplashScreen.isFinished;
            if (_startSplash && !_inProgress)
            {
                StartCoroutine(SplashEffectStart());
                _inProgress = true;

            }
        }

        IEnumerator SplashEffectStart()
        {
            SplashDIalog.SetActive(true);
            while (_handTracked)
            {
                //Do nothing until the hand is Tracked
                StartCoroutine(GetHandTrackingState());
            }
            yield return new WaitForSeconds(1);
            //Load Scene Asynchronous
            yield return null;
        }

        IEnumerator GetHandTrackingState()
        {

            _handTracked = OVRInput.GetActiveController() == OVRInput.Controller.Hands;
            yield return null;
        }
    }
}
