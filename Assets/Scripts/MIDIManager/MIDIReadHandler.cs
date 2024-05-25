using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using ImmersivePiano;
using DemoMPTK;
using System.Threading.Tasks;
using System;
using Unity.VisualScripting;
using ImmersivePiano.MIDI;
using System.Linq;

namespace ImmersivePiano.MIDI
{
    /// <summary>
    /// @brief Handlen the MIDI File reading process
    /// </summary>
    public class MIDIReadHandler : MonoBehaviour
    {
        [Header("Reference")]
        /// <summary>
        /// Play MIDI Music from the MIDI file
        /// </summary>
        public MidiFilePlayer midiFilePlayer;
        [SerializeField] MIDISystemManagement manager;
        /// <summary>
        /// Playing Generated Music from Algorithm along with MPTK Event
        /// </summary>
        //public MidiStreamPlayer midiStreamPlayer;
        [SerializeField] GameObject spawnParent;


        #region Private props
        private string _midiFileName = "Yiruma - Rivers Flow In You _1_";
        //private List<MPTKEvent> events = new List<MPTKEvent>();
        private List<MPTKEvent> eventsCollected = new List<MPTKEvent>();
        private List<Transform> spawnPos;
        #endregion

        private void Start()
        {
            //At first sight, check if SoundFont is loaded
            if (!HelperDemo.CheckSFExists()) { return; }

            if (midiFilePlayer == null)
            {
                midiFilePlayer = FindAnyObjectByType<MidiFilePlayer>();
            }

            if (spawnPos != null)
            {
                foreach (Transform t in spawnParent.transform)
                {
                    spawnPos.Add(t);
                }
            }
            if (manager == null)
            {
                manager = FindAnyObjectByType<MIDISystemManagement>();
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReadMidiFileAsync();
            }
        }

        /// <summary>
        /// Start reading the MIDI FIle
        /// </summary>
        async void ReadMidiFileAsync()
        {
            midiFilePlayer.MPTK_MidiName = _midiFileName;
            //Get all MIDI events before start playing
            MidiLoad result = midiFilePlayer.MPTK_Load();
            if (result != null)
            {
                Debug.Log($"Collected {result.MPTK_ReadMidiEvents().Count} events in the midi File");
                await Task.Delay(TimeSpan.FromSeconds(3));
                StartCoroutine(manager.SpawningNotes(result.MPTK_ReadMidiEvents()));
            }
            //midiFilePlayer.MPTK_Play();
        }



        /// <summary>
        /// Get access to all the MIDI events in the MIDI File
        /// </summary>
        /// <param name="events"></param>
        //public void NotesToPlay(List<MPTKEvent> events)
        //{
        //    events = midiFilePlayer.MPTK_MidiEvents;

        //    Debug.Log("Received " + events.Count+ " MPTK Events");

        //    foreach (MPTKEvent e in events)
        //    {
        //        if(e.Command == MPTKCommand.NoteOn)
        //        {
        //            Debug.Log($"Note on Time: {e.RealTime} millisecond" + //Value to schedule the spawning
        //                $"Note : {e.Value}" + //MIDI Notes value C4 = 60
        //                $"Duration : {e.Duration} milliseconds" + //The length of the note
        //                $"Velocity: {e.Velocity}" + //How hard the key will be pressed
        //                $"Create Time: {e.CreateTime}"); //Not really important
        //        }
        //    }
        //    midiFilePlayer.MPTK_Play();
        //}

        public List<MPTKEvent> GetListEvents()
        {
            return midiFilePlayer.MPTK_ReadMidiEvents();
        }
        public List<Transform> GetPublicTransform()
        {
            return spawnPos;
        }
    }
}
