using ImmersivePiano.MIDI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongButtonHandler : MonoBehaviour
{
    [SerializeField] string _midiFileName;
    [SerializeField] Songs songs;
    [SerializeField] string _songName;
    private Toggle _toggle;

    private void Awake()
    {
        try
        {
        var SongInfo = transform.GetChild(1);
        _songName = SongInfo.GetChild(0).GetComponent<TMP_Text>().text;
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
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
        PlaySongButtonHandler.instance.CurrentMIDI = _midiFileName;
        PlaySongButtonHandler.instance.CurrentSongName = _songName;
    }
}
