using MidiPlayerTK;
using MPTK.NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CancellationToken = System.Threading.CancellationToken;

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
        [SerializeField] NotesInPraciceHandler notesInPraciceHandler;
        [SerializeField] GameObject spawnParent;
        [SerializeField] GameObject notesStorage;
        [SerializeField] Transform endPos;
        [SerializeField] bool freestyle;
        [SerializeField] bool flowContinue = true;
        private float speed;

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
        private float _spawnOffset = 0.02f;
        private MIDIKey[] midiKeys;

        private float _initFreestyleSpeed = 0.1f;
        private float _initPracticeSpeed = 0.5f;
        //private MIDIKey[] _keys;

        #endregion
        public bool FlowContinue
        {
            get { return flowContinue; }
            set { flowContinue = value; }
        }
        public bool IsFreestyle
        {
            get { return freestyle; }
            set
            {
                freestyle = value;
                speed = freestyle ? _initFreestyleSpeed : _initPracticeSpeed;
                Debug.Log($"Current Speed : {speed}");
                notesInPraciceHandler.enabled = !freestyle;
            }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        //public MIDIKey[] Keys
        //{
        //    get { return _keys; }
        //    set { _keys = value; }
        //}
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
            speed = IsFreestyle ? _initFreestyleSpeed : _initPracticeSpeed;
            midiKeys = FindObjectsOfType<MIDIKey>();
        }
        private void Update()
        {

        }

        /// <summary>
        /// @brief Spawning falling music notes
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public IEnumerator SpawningNotes(List<MPTKEvent> events, CancellationToken cancellationToken)
        {
            yield return new WaitForSeconds(3);
            foreach (MPTKEvent e in events)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }
                switch (e.Command)
                {
                    //Extract the note data
                    case MPTKCommand.NoteOn:
                        if (e.Value >= 21 && e.Value <= 108)
                        {
                            //Divide the note into its matching key by value
                            Transform t = LaneList.Find(item => item.name.ToString() == e.Value.ToString());

                            #region Spawning, customizing the notes
                            MIDINote newNote = Instantiate(notePrefab, t.position, /*Quaternion.Euler(0, 0, 0)*/ Quaternion.identity, notesStorage.transform).transform.GetChild(0).GetComponent<MIDINote>();
                            newNote.transform.parent.localRotation = Quaternion.identity;
                            newNote.gameObject.name = e.Value.ToString();
                            newNote.MIDINoteSet(e.RealTime, e.Value, e.Duration, e.Velocity, speed); // not necess
                            newNote.MIDIKey = midiKeys.FirstOrDefault(midiKey => midiKey.KeyValue == e.Value); //set the matching midi key
                            newNote.gameObject.SetActive(true);
                            newNote.hideFlags = HideFlags.HideInHierarchy;
                            newNote.SetMIDIStreamPlayer(midiStreamPlayer);
                            newNote.SetMPTKEvent(e);
                            #endregion

                            StartCoroutine(StartFlowingAfter(e.RealTime, newNote.gameObject,cancellationToken));
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

        /// <summary>
        /// Spawn a note by the time of a key being pressed
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public MIDINote SpawningNote(Transform t, MIDIKey key)
        {
            if (freestyle)
            {
                try
                {
                    Vector3 pos = t.position;
                    pos.z += _spawnOffset;
                    //GameObject note = Instantiate(AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDBs.NoteFake.AssetGUID)), t) as GameObject;
                    MIDINote note = Instantiate(notePrefab, pos, /*Quaternion.Euler(0, 0, 0)*/Quaternion.identity, notesStorage.transform).transform.GetChild(0).GetComponent<MIDINote>();
                    note.transform.parent.localRotation = Quaternion.identity;
                    note.name = $"NoteKey {key.KeyValue}";
                    return note;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
            return null;
        }

        public void ClearPreviousSong()
        {
            StopAllCoroutines();
            midiFilePlayer.MPTK_ClearAllSound();
            foreach (Transform child in notesStorage.transform)
            {
                Destroy(child.gameObject);
            }
        }
        public IEnumerator StartFlowingAfter(float NoteActivationTime, GameObject note, CancellationToken cancellationToken)
        {
            note.SetActive(false);
            yield return new WaitForSeconds(NoteActivationTime / 1000);
            if (cancellationToken.IsCancellationRequested)
            {
                yield break;
            }
            note.SetActive(true);
        }
        public IEnumerator StartFlowingAfter(float NoteActivationTime, float newSpeed)
        {
            yield return new WaitForSeconds(NoteActivationTime / 1000);
            if (speed != newSpeed)
            {
                //speed = Mathf.Lerp(speed, newSpeed, Time.deltaTime);
                speed = newSpeed;
                //speed = lerpValue;
            }
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

            // Calculate distance
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

        public Transform GetEndPos()
        {
            return endPos;
        }

        public Transform GetSpawnPos()
        {
            return spawnParent.transform;
        }
    }
}


