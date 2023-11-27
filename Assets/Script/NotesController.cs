using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject notes;
    public AudioSource audioSource;

    private void Start()
    {
        DisableNotes();
    }

    public void EnableNotes()
    {
        audioSource.volume = 1;
        notes.SetActive(true);
        particleSystem.Play();
        audioSource.Play();
        StartCoroutine(DecreaseVolume());
        
    }

    public void DisableNotes()
    {
        notes.SetActive(false);
        particleSystem.Stop();
        StopAllCoroutines();
    }

    IEnumerator DecreaseVolume()
    {
        yield return new WaitForSeconds(4);
        Debug.Log("HAha");
        while (audioSource.volume > 0.01)
        {
            audioSource.volume -= 0.005f;
            Debug.Log("true");
            yield return null;
        }
    }
}
