using MidiPlayerTK;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace ImmersivePiano.MIDI
{
    /// <summary>
    /// @brief Note spawned from midi file
    /// A MIDINote is the management to each value read from each note in the MIDI file
    /// Comes along as "MPTKEvent" with beat, ticks, duration, start time,...
    /// </summary>

    public class MIDINote : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] float spawnTime;
        [SerializeField] int noteValue;
        [SerializeField] long duration;
        [SerializeField] int velocity;
        [SerializeField] MidiStreamPlayer midiStreamPlayer;

        [Header("Reference")]
        [SerializeField] MPTKEvent note;
        [SerializeField] Transform originalPos;
        [SerializeField] MIDIKey matchingKey;

        private Transform par;
        private float speed;
        private bool _isSpawnedDone;
        private float _pressStartTime;
        //private float delta = 0f;
        //private Vector3 initialScale
        //

        public MIDIKey MIDIKey
        {
            get { return matchingKey; }
            set { matchingKey = value; }
        }

        public bool IsSpawnedDone
        {
            get { return _isSpawnedDone; }
            set { _isSpawnedDone = value; }
        }

        public float PressStartTime
        {
            get { return _pressStartTime; }
            set { _pressStartTime = value; }
        }
        private void Awake()
        {
            par = transform.parent;
            //initialScale = transform.localScale;
        }

        public void MIDINoteSet(float time, int value, long dur, int velo, float speed)
        {
            this.duration = dur; //in miliseconds
            this.spawnTime = time;
            this.noteValue = value;
            this.velocity = velo;
            this.speed = speed;
            NoteLenghtAdjust();
        }
        public void SetMIDIStreamPlayer(MidiStreamPlayer midiStreamPlayer)
        {
            this.midiStreamPlayer = midiStreamPlayer;
        }
        public void SetMPTKEvent(MPTKEvent enote)
        {
            this.note = enote;
        }
        public MPTKEvent GetMPTKEvent()
        {
            return this.note;
        }
        public void SpawnedNoteLengthAdjust(MIDIKey selfkey)
        {
            MIDIKey = selfkey;
            //while (matchingKey.IsPressed)
            //{
            //    var par = gameObject.transform.parent;
            //    float temp = par.transform.localScale.y;
            //    par.transform.localScale = new Vector3(1f, temp += MIDISystemManagement.instance.speed * 2, 1f);
            //    //par.transform.localScale = new Vector3(1f, temp += MIDISystemManagement.instance.speed * Time.deltaTime / 1000f, 1f);

            //}
        }
        public void NoteLenghtAdjust()
        {
            var par = gameObject.transform.parent;
            par.transform.localScale = new Vector3(1, 1, duration / 8);
        }

        //Destroy, scale up the note
        private void Update()
        {
            if (MIDISystemManagement.instance.IsFreestyle)
            {
                if (transform.position.z >= MIDISystemManagement.instance.GetSpawnPos().position.z)
                {
                    Destroy(par.gameObject);
                }
                //Scale up the note if Freestyle
                if (!IsSpawnedDone)
                {
                    //Vector3 scale = par.localScale;
                    //scale.z += MIDISystemManagement.instance.speed * 2 * Time.deltaTime;
                    //par.localScale = scale;
                    Vector3 scale = transform.localScale;
                    scale.y += MIDISystemManagement.instance.Speed * 2 * Time.deltaTime;
                    transform.localScale = scale;
                }
            }
            else
            {
                if (transform.position.z <= MIDISystemManagement.instance.GetEndPos().position.z)
                {
                    midiStreamPlayer.MPTK_PlayEvent(note); //
                    Destroy(par.gameObject);
                }
            }
        }

        //Move the note
        private void FixedUpdate()
        {
            if (MIDISystemManagement.instance.IsFreestyle)
            {
                if(!IsSpawnedDone)
                {
                    float translation = Time.deltaTime * MIDISystemManagement.instance.Speed;
                    transform.Translate(0, -translation, 0);
                }
                else
                {
                    float translation = Time.deltaTime * MIDISystemManagement.instance.Speed * 2;
                    transform.Translate(0, -translation, 0);
                }
                //par.Translate(0, 0, translation);;
            }
            else
            {
                float translation = Time.deltaTime * MIDISystemManagement.instance.Speed;
                transform.Translate(0, translation, 0);
                //par.Translate(0, 0, -translation);

            }
        }


    }
}
