using MidiPlayerTK;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Read the MIDI File by assigning the midi file name, then press Space and A, copy the content in the console
/// </summary>
public class MIDIFileReadToList : MonoBehaviour
{
    [Tooltip("Drag the MIDIFilePlayer Object into this field")]
    [SerializeField]
    private MidiFilePlayer midiFilePlayer;

    private List<string> contentList = new();

    [Tooltip("The name of the midi file to be read, paste the name into this field while running to read it")]
    public string _midiName;

    private void Start()
    {
        if (midiFilePlayer == null)
        {
            midiFilePlayer = FindAnyObjectByType<MidiFilePlayer>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_midiName != null)
            {
                //var content = StartCoroutine(MIDIFileContentTolist());
                //Debug.Log(content);
                StartCoroutine(MIDIFileContentTolist());
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_midiName != null)
            {
                string commaSeparatedString = string.Join(",", contentList);
                Debug.Log(commaSeparatedString);
            }
        }
    }


    private IEnumerator MIDIFileContentTolist()
    {
        midiFilePlayer.MPTK_MidiName = _midiName;
        //Get all MIDI events before start playing
        MidiLoad result = midiFilePlayer.MPTK_Load();


        foreach (MPTKEvent e in result.MPTK_ReadMidiEvents())
        {
            switch (e.Command)
            {
                //Extract the note data
                case MPTKCommand.NoteOn:
                    if (e.Value >= 21 && e.Value <= 108)
                    {
                        //contentList.Add(e.Value.ToString());
                        contentList.Add("\"" + e.Value.ToString() + "\"");
                    }
                    break;

                case MPTKCommand.MetaEvent:
                    break;
            }
        }
        yield return contentList;
    }
}

