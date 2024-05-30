using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] GameObject NoteSheet;
    [SerializeField] GameObject Dialogs;
    [SerializeField] GameObject CenterEyeAnchor;
    private bool isCorrect;
    // a common Monobehaviour script start from awake - on enable - start - update - on disable - on destroy
    private void Start()
    {
        isCorrect = false;
    }
    private void Update()
    {
        if (!isCorrect)
        {
            Display();
        }
        else
        {
            
        }
    }

    private void Display()
    {
        //note sheet should be the same height as the player, 40 centimeters away from them , straight ahead
        //dialog should be the same height as the player, 30 centimeters away from them, straight ahead, 20 centimeters above the note sheet
        Vector3 pos = CenterEyeAnchor.transform.position;
        Quaternion rot = CenterEyeAnchor.transform.rotation;
        pos.y -= 0.05f;
        pos.z += 0.5f;
        NoteSheet.transform.position = pos;
        Dialogs.transform.position = Vector3.Lerp(Dialogs.transform.position, pos, Time.deltaTime);
        //Dialogs.transform.rotation = Quaternion.Euler(Dialogs.transform.rotation.x,Mathf.Lerp(Dialogs.transform.rotation.y, rot.y,Time.deltaTime), rot.z);
    }
    public void SetCorrect()
    {
        isCorrect = true;
    }
}
