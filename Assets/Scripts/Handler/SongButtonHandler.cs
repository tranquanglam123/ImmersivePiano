using ImmersivePiano.MIDI;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SongButtonHandler : MonoBehaviour
{
    [SerializeField] string _midiFileName;
    [SerializeField] Songs songs;
    private Toggle _toggle;

    void Start()
    {
        //Fetch the Toggle GameObject
        _toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        _toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }

    private void ToggleValueChanged()
    {
        OldMethod();
    }

    public string MIDIFileName
    {
        get { return _midiFileName; }
        set { _midiFileName = value; }
    }

    void OldMethod()
    {
        //NoteHighlighterManager.instance.SetSong(songs);
        //MIDIReadHandler.instance.midiFileName = _midiFileName;
        //MIDIReadHandler.instance.ReadMidiFileAsync();
        PlaySongButtonHandler.instance.CurrentSong = _midiFileName;
    }
}
