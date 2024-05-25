using MidiPlayerTK;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

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
        [SerializeField] bool isPlayed = false;
        [SerializeField] MidiStreamPlayer midiStreamPlayer;

        [Header("Reference")]
        [SerializeField] MPTKEvent note;
        [SerializeField] Transform originalPos;

        private float speed;
        //private float delta = 0f;
        //private Vector3 initialScale;

        private void Awake()
        {
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
        private void Update()
        {
            if (transform.position.z <= 0.25f)
            {
                midiStreamPlayer.MPTK_PlayEvent(note); //
                Destroy(gameObject);
            }
            //else
            //{
            //    float distance = Mathf.Abs(transform.position.z - pianoBarZPosition); // Replace with actual bar Z position
            //    float volume = Mathf.Clamp01(1 - distance / triggerDistance); // Adjust triggerDistance as needed
            //    //midiStreamPlayer.SetVolume(volume); //distance based fading
            //}
        }

        public void NoteLenghtAdjust()
        {
            var par = gameObject.transform.parent;
            par.transform.localScale = new Vector3(1, 1, duration / 10);
        }
        private void FixedUpdate()
        {
            float translation = Time.deltaTime * MIDISystemManagement.instance.speed;
            transform.Translate(0, translation, 0);
        }


    }
}
