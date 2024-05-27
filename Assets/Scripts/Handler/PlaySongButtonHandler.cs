using ImmersivePiano.MIDI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySongButtonHandler : Singleton<PlaySongButtonHandler>
{
    private string _curSong;
    private Toggle _toggle;
    public string CurrentSong
    {
        get { return _curSong; }
        set { _curSong = value; }
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
        //try
        //{
        //    MIDIReadHandler.instance.midiFileName = _curSong;
        //    MIDIReadHandler.instance.ReadMidiFileAsync();
        //}
        //catch (Exception ex)
        //{
        //    Debug.LogException(ex);
        //}
    }
}