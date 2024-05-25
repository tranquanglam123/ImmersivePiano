using MidiPlayerTK;
using MPTK.NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ImmersivePiano.MIDI
{
    /// <summary>
    /// @brief Manager of the whole MIDI System
    /// Control the spawn notes and played notes
    /// </summary>
    public class MIDISystemManagement : Singleton<MIDISystemManagement>
    {
        #region
        [Header("Config")]
        [SerializeField] MidiFilePlayer midiFilePlayer;
        [SerializeField] MidiStreamPlayer midiStreamPlayer;
        [SerializeField] MIDIReadHandler mIDIReadHandler;
        [SerializeField] GameObject spawnParent;
        [SerializeField] GameObject notesStorage;
        [SerializeField] Transform endPos;
        public float speed = 0.65f;

        [Header("System tweaks")]
        //[SerializeField] Material spawnColor;
        [SerializeField] GameObject notePrefab;

        [Header("Monitoring")]
        public MIDINote[] countNotes;
        public List<Transform> transforms;

        //private int referenceDur;
        private readonly List<Transform> LaneList = new();
        private int numerator;
        private int denumerator;
        private int curTempo = 500000;
        private int deltaTicksPerQuarterNote;
        #endregion


        private void Start()
        {
            if (midiFilePlayer == null)
            {
                midiFilePlayer = FindAnyObjectByType<MidiFilePlayer>();
            }
            if (mIDIReadHandler == null)
            {
                mIDIReadHandler = FindAnyObjectByType<MIDIReadHandler>();
            }
            foreach (Transform child in spawnParent.transform)
            {
                if (child != null)
                {
                    LaneList.Add(child);
                }
            }
            deltaTicksPerQuarterNote = midiFilePlayer.MPTK_DeltaTicksPerQuarterNote;
            if (deltaTicksPerQuarterNote == 0)
            {
                deltaTicksPerQuarterNote = 48;
            }
        }
        private void Update()
        {

        }

        /// <summary>
        /// @brief Spawning falling music notes
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public IEnumerator SpawningNotes(List<MPTKEvent> events)
        {
            foreach (MPTKEvent e in events)
            {
                switch (e.Command)
                {
                    //Extract the note data
                    case MPTKCommand.NoteOn:
                        if (e.Value >= 21 && e.Value <= 108)
                        {
                            //Divide the note into its matching key by value
                            Transform t = LaneList.Find(item => item.name.ToString() == e.Value.ToString());

                            #region Spawning, customizing the notes
                            MIDINote newNote = Instantiate(notePrefab, t.position, Quaternion.Euler(0, 0, 0), notesStorage.transform).transform.GetChild(0).GetComponent<MIDINote>();
                            newNote.MIDINoteSet(e.RealTime, e.Value, e.Duration, e.Velocity, speed);
                            newNote.gameObject.SetActive(true);
                            newNote.hideFlags = HideFlags.HideInHierarchy;
                            newNote.SetMIDIStreamPlayer(midiStreamPlayer);
                            newNote.SetMPTKEvent(e);
                            #endregion

                            StartCoroutine(StartFlowingAfter(e.RealTime, newNote.gameObject));
                        }
                        break;

                    //Extract the new configs value
                    case MPTKCommand.MetaEvent:
                        try
                        {
                            switch (e.Meta)
                            {
                                case MPTKMeta.KeySignature: break;
                                case MPTKMeta.TimeSignature:
                                    numerator = (int)(MPTKEvent.ExtractFromInt(((uint)e.Value), 0));
                                    denumerator = (int)(Mathf.Pow(2, MPTKEvent.ExtractFromInt(((uint)e.Value), 1)));
                                    break;
                                case MPTKMeta.SetTempo:
                                    curTempo = e.Value;
                                    Debug.Log($"curTempo: {curTempo}");
                                    //CalculateMPT(curTempo, deltaTicksPerQuarterNote);
                                    var tempspeed = CalculateSpeed();
                                    Debug.Log($"Calculated Speed: {tempspeed}");

                                    StartCoroutine(StartFlowingAfter(e.RealTime, tempspeed));
                                    //StartCoroutine(StartFlowingAfter(e.RealTime, CalculateSpeed()));
                                    break;
                                default: break;
                            }
                        }
                        catch (NullReferenceException)
                        {
                            throw new Exception("Null Error");
                        }
                        break;


                }
            }
            transforms = getTransformList();
            yield return null;
        }

        public IEnumerator StartFlowingAfter(float NoteActivationTime, GameObject note)
        {
            note.SetActive(false);
            yield return new WaitForSeconds(NoteActivationTime / 1000);
            note.SetActive(true);
        }
        public IEnumerator StartFlowingAfter(float NoteActivationTime, float newSpeed)
        {
            yield return new WaitForSeconds(NoteActivationTime / 1000);
            speed = newSpeed;
            Debug.Log($"New Speed : {speed}");
        }
        public List<Transform> getTransformList()
        {
            return LaneList;
        }

        int CalculateMPT(int microsecondsperQNote, int ticksperQNote)
        {
            return microsecondsperQNote / ticksperQNote;
        }
        float CalculateSpeed()
        {
            float temp1 = (float)curTempo / 1000000f;

            // Calculate temp2
            var temp2 = spawnParent.transform.position.z - endPos.position.z;

            // Check for zero before division
            if (Mathf.Approximately(temp1, 0f))
            {
                // Handle zero case (e.g., set default speed)
                return 0.65f; // Default speed if temp1 is zero
            }
            return temp2 / temp1;
        }

        public MidiStreamPlayer GetMidiStreamPlayer()
        {
            return midiStreamPlayer;
        }

    }
}


