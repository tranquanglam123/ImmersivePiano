using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImmersivePiano.MIDI
{
    /// <summary>
    /// @brief Manage the lane for MIDI Spawn
    /// Lane is each line straightforward from any key of the piano
    /// It aims to serve as a moving line for the notes for visualization
    /// </summary>
    public class MIDILane : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] readonly int NoteRestriction;
        [SerializeField] Transform FirstPoint;
        [SerializeField] Transform LastPoint;
    }

}
