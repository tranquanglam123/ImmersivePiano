using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPage : MonoBehaviour
{
    [SerializeField] private Transform rightHand;
    [SerializeField] private float _turnDistance;
    [SerializeField] private AudioSource _audioSource;

    private Vector3 _rightHandStartPosision;
    public static bool _isChecking = false;
    private bool _isTurned = false;
    private float _pagePositionX;

    public static Action OnTurnComplete;

    private void Update()
    {
        if (_isChecking && !_isTurned)
        {
            if (!_isTurned)
            {
                float currentDistance = _rightHandStartPosision.x - rightHand.position.x ;
                currentDistance = Mathf.Clamp(currentDistance, 0, Mathf.Infinity);
                float currentDistanceNormalized = currentDistance;

                transform.position = new Vector3(_pagePositionX - currentDistanceNormalized, transform.position.y, transform.position.z);

                //_animator.speed = 0;
                if (currentDistance > _turnDistance)
                {
                    _isTurned = true;
                    transform.position = new Vector3(_pagePositionX, transform.position.y, transform.position.z);
                    OnTurnComplete?.Invoke();
                }
            }
        }
    }

    //Editor UnityEvent
    public void StartCheckingTuringPageDistance()
    {
        Debug.Log($"is checking {_isChecking}");
        
        if (_isChecking)
            return;
        
        _audioSource.Play();
        _isTurned = false;
        _isChecking = true;
        _rightHandStartPosision = rightHand.position;
        _pagePositionX = transform.position.x;
    }
    
    
}
