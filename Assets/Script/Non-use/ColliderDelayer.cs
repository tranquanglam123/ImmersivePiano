using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDelayer : MonoBehaviour
{
    private Collider _collider;
    private void OnEnable()
    {
        if (_collider == null)
            _collider = GetComponent<Collider>();

        _collider.enabled = false;
        StartCoroutine(Delayer(1));
    }

    private IEnumerator Delayer(int timeDelayed)
    {
        yield return new WaitForSeconds(timeDelayed);
        _collider.enabled = true;
    }
}
