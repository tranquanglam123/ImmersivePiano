using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecs : MonoBehaviour
{
    public int secs;
    [SerializeField] new GameObject gameObject;
    private void Start()
    {
        Wait(secs);
    }
    IEnumerator Wait(int secs)
    {
        yield return new WaitForSeconds(secs);
    }

    public void WaitAndLoad(int secs)
    {
        StartCoroutine(Wait(secs));
        gameObject.SetActive(false);
    }
}
