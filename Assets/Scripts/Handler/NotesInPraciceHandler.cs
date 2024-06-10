using ImmersivePiano;
using ImmersivePiano.MIDI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NotesInPraciceHandler : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    // Check if colliding object has a MIDINote component
    //    if (other.TryGetComponent<MIDINote>(out var midiNote))
    //    {
    //        //// Add the MIDINote to the list if not already present
    //        //if (!currentPlayingNotes.Contains(midiNote))
    //        //{
    //        //    currentPlayingNotes.Add(midiNote);
    //        //}
    //        MIDISystemManagement.instance.GetMidiStreamPlayer().MPTK_PlayEvent(midiNote.MPTKEvent);
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        // Check if colliding object has a MIDINote component
        if (other.TryGetComponent<MIDINote>(out var midiNote))
        {
            // Remove the MIDINote from the list
            //currentPlayingNotes.RemoveAt(currentPlayingNotes.IndexOf(midiNote));
            Destroy(midiNote.gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other == null)
        {
            MIDISystemManagement.instance.FlowContinue = true;
        }
        if ((other.TryGetComponent<MIDINote>(out var midiNote)))
        {
            if (midiNote != null)
            {
                if (!midiNote.MIDIKey.IsPressed)
                {
                    MIDISystemManagement.instance.FlowContinue = false;
                }
                else
                {
                    MIDISystemManagement.instance.FlowContinue = true;
                }
            }
        }
    }
}
