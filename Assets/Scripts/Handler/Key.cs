using ImmersivePiano.MIDI;
using MidiPlayerTK;
using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ImmersivePiano
{
    public class Key : MonoBehaviour, IPressable
    {
        
        [SerializeField] int keyValue;
        private MPTKEvent noteEvent;

        #region MPTKEvent params
        private MPTKCommand command = MPTKCommand.NoteOn;
        private int channel = 0;
        private float duration = 0;
        private float velocity = 0;
        private long delay = 0;
        #endregion


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
        }

        public void StopPressing()
        {
            MIDISystemManagement.instance.GetMidiStreamPlayer().MPTK_StopEvent(noteEvent);
        }
    }
}
