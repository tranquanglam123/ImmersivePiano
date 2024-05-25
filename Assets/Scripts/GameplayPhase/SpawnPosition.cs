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
        pos.y -= 0.2f;
        pos.z += 0.5f;
        NoteSheet.transform.position = pos;
        Dialogs.transform.position = pos;
        ////set the rotation of the note sheet and dialog to face directly at the player without turning 180 degrees
        //NoteSheet.transform.LookAt(CenterEyeAnchor.transform);
        //NoteSheet.transform.Rotate(0, -180, 0);
        //Dialogs.transform.LookAt(CenterEyeAnchor.transform);
        //Dialogs.transform.Rotate(0, -180, 0);
    }
    public void SetCorrect()
    {
        isCorrect = true;
    }
}
