using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNoteObj : MonoBehaviour
{
    public Material spawnMat;
    public Material pressedMat;
    public void InitGameNote(float timeOfNote, int noteNumber, float duration, float instrument)
    {
        transform.position = new Vector3(timeOfNote, -noteNumber);
        transform.localScale = new Vector3 (transform.localScale.x,duration, transform.localScale.z);
        GetComponent<MeshRenderer>().material = spawnMat;

    }
}
