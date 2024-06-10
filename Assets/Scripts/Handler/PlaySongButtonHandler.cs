using ImmersivePiano.MIDI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySongButtonHandler : Singleton<PlaySongButtonHandler>
{
    private string _curMIDI;
    private string _curSongName;
    private Toggle _toggle;
    public GameObject Songmenu;
    public CurrentSongDisplayHandler CurrentSongDisplay;
    public string CurrentMIDI
    {
        get { return _curMIDI; }
        set { _curMIDI = value; }
    }
    public string CurrentSongName
    {
        get { return _curSongName; }
        set { _curSongName = value; }
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
        if (CurrentSongDisplay != null)
        {
            try
            {
                MIDISystemManagement.instance.IsFreestyle = false;
                MIDISystemManagement.instance.FlowContinue = true;
                MIDIReadHandler.instance.midiFileName = _curMIDI;
                CurrentSongDisplay.StartShowingCurSong(_curSongName);
                MIDIReadHandler.instance.ReadMidiFileAsync();
                Songmenu.SetActive(false);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
