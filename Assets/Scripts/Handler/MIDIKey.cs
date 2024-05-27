using ImmersivePiano.MIDI;
using MidiPlayerTK;
using Oculus.Interaction;
using Oculus.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ImmersivePiano
{
    public class MIDIKey : MonoBehaviour, IPressable
    {
        
        [SerializeField] int keyValue;
        private MPTKEvent noteEvent;
        private bool isNoteSpawned = false;
        private bool _isPressed;
        private MIDINote curVisualizedNote;
        #region MPTKEvent params
        private MPTKCommand command = MPTKCommand.NoteOn;
        private int channel = 0;
        private float duration = 0;
        private float velocity = 0;
        private long delay = 0;
        #endregion

        public int KeyValue
        {
            get { return keyValue; }
            set { keyValue = value; }
        }

        public bool IsPressed
        {
            get { return _isPressed; }
            set { _isPressed = value; }
        }

        private void OnEnable()
        {
            noteEvent = new MPTKEvent()
            {
                Command = command,
                Value = keyValue,
                Channel = channel,
                Delay = delay
            };
        }
        private void Start()
        {
            try
            {
                transform.GetChild(0).gameObject.GetComponent<PokeInteractable>();
            }
            catch (NullReferenceException) { }
        }
       
        public void Press()
        {
            MIDISystemManagement.instance.GetMidiStreamPlayer().MPTK_PlayEvent(noteEvent);
            if(!isNoteSpawned )
            {
                MIDINote newnote = MIDISystemManagement.instance.SpawningNote(transform, this);
                //newnote.SpawnedNoteLengthAdjust(this);
                newnote.PressStartTime = Time.time;
                curVisualizedNote = newnote;
                curVisualizedNote.IsSpawnedDone = false;
                isNoteSpawned = true;
                IsPressed = true;
            }
            
        }

        public void StopPressing()
        {
            MIDISystemManagement.instance.GetMidiStreamPlayer().MPTK_StopEvent(noteEvent);
            isNoteSpawned=false;
            IsPressed = false;
            curVisualizedNote.IsSpawnedDone = true;
            curVisualizedNote = null; 
        }

    }
}
