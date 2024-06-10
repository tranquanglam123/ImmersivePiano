using ImmersivePiano.MIDI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSongDisplayHandler : MonoBehaviour
{
    public GameObject SongsMenu;
    
    [SerializeField] GameObject _curSongTxt;
    [SerializeField] Button _button;

    private void Awake()
    {
        try
        {
            _button.onClick.AddListener(delegate
            {
                StopPlayingCurSong();
            });
        }
        catch (Exception ex) 
        {
            Debug.LogException(ex);
        }
    }
    public void StopPlayingCurSong()
    {
        MIDISystemManagement.instance.ClearPreviousSong();
        _curSongTxt.GetComponent<TMP_Text>().SetText(null, true);
        _button.gameObject.SetActive(false);
        SongsMenu.SetActive(true);
    }

    public void StartShowingCurSong(string name)
    {
        _curSongTxt.GetComponent<TMP_Text>().SetText(name, true);
        _button.gameObject.SetActive(true);
    }
}
