using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHighlighterManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private List<MusicNotes> _musicNotes;

    private GameObject[] _keys;

    private MusicNotes _currentMusicNote;
    private string[] _currentNoteSequence;
    private List<GameObject> _currentHighlighters;
    private List<GameObject> _currentPages;

    private int _hilighterCounter;
    private int _pageCounter;
    private GameObject _currentKey;
    private bool _isPaused = false;

    // Start is called before the first frame update
    private void Awake()
    {
        _hilighterCounter = 0;
        _pageCounter = 0;
        PrepareKeys();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SetNextKey();
    }

    private void PrepareKeys()
    {
        _keys = GameObject.FindGameObjectsWithTag("Key");
        foreach (GameObject key in _keys)
        {
            Debug.Log(key.name);
            key.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void SetNextKey()
    {
        if (_currentNoteSequence.Length == 0 || _hilighterCounter >= _currentNoteSequence.Length)
            return;

        var currentNote = _currentNoteSequence[_hilighterCounter];
        foreach (GameObject key in _keys)
        {
            if (key.GetComponent<Key>().keyName == currentNote)
                _currentKey = key;
        }

        _hilighterCounter++;
        
        if(CheckForPageSeparators())
            return;

        if (CheckForWin())
            return;

        SetNextHighligher();
    }

    

    private void SetNextHighligher()
    {
        if(_currentHighlighters.Count == 0 || _hilighterCounter == _currentHighlighters.Count)
            return;
        
        // Highlighting notes on pages
        var currentHighlighter = _currentHighlighters[_hilighterCounter];
        foreach (GameObject highlighter in _currentHighlighters)
        {
            highlighter.SetActive(false);
        }
        _currentHighlighters[_hilighterCounter].SetActive(true);
        
        // Highlighting keys on piano
        foreach (GameObject key in _keys)
        {
            if(key.GetComponent<Key>().child1 != null)
                key.GetComponent<Key>().child1.SetActive(false);
        }
        
        foreach (GameObject key in _keys)
        {
            if(key.GetComponent<Key>().child1 != null && key.GetComponent<Key>().keyName == _currentNoteSequence[_hilighterCounter])
                key.GetComponent<Key>().child1.SetActive(true);
        }
    }

    private bool CheckForWin()
    {
        if (_hilighterCounter == _currentNoteSequence.Length)
        {
            _particleSystem.Play();
            Debug.Log("Particle");
            
            foreach (GameObject highlighter in _currentHighlighters)
            {
                highlighter.SetActive(false);
            }
            
            foreach (GameObject  key in _keys)
            {
                if(key.GetComponent<Key>().child1 != null)
                    key.GetComponent<Key>().child1.SetActive(false);
            }

            return true;
        }
        return false;
    }
    
    private bool CheckForPageSeparators()
    {
        if (_currentMusicNote.PageSeparatorsByNote.Length == 0)
            return false;

        foreach (int pageSeparator in _currentMusicNote.PageSeparatorsByNote)
        {
            if (pageSeparator == _hilighterCounter)
            {
                PauseHighlighing();
                return true;
            }
        }

        return false;
    }
    
    void SetNextPage()
    {
        if(_currentPages.Count == 0 || _pageCounter == _currentPages.Count)
            return;

        foreach (GameObject currentPage in _currentPages)
        {
            currentPage.SetActive(false);
        }
        
        _currentPages[_pageCounter].SetActive(true);
        _pageCounter++;
    }

    void PauseHighlighing()
    {
        _isPaused = true;
        foreach (GameObject highlighter in _currentHighlighters)
        {
            highlighter.SetActive(false);
        }
        foreach (GameObject key in _keys)
        {
            if(key.GetComponent<Key>().child1 != null)
                key.GetComponent<Key>().child1.SetActive(false);
        }
        _currentMusicNote.pageTurner.SetActive(true);
    }

    void ResumeHilighting()
    {
        _isPaused = false;
        SetNextHighligher();
        SetNextPage();
        _currentMusicNote.pageTurner.SetActive(false);
    }
    
    //Editor UnityEvent
    public void CheckKey()
    {
        if( Key.LastPressedKey == null || _currentNoteSequence == null)
            return;
        
        if (!_isPaused && Key.LastPressedKey == _currentNoteSequence[_hilighterCounter])
        {
            SetNextKey();
        }
    }
    
    public void RestartNotes()
    {
        _hilighterCounter = 0;
        _pageCounter = 0;
        TurnPage._isChecking = false;
        _isPaused = false;
        _currentMusicNote.pageTurner.SetActive(false);
        SetNextHighligher();
        SetNextPage();
    }

    //Editor UnityEvent
    public void SetSong(int songNumber)
    {
        if (_currentMusicNote != null)
            _currentMusicNote.DeactivatePagesAndHighlighers();

        _currentMusicNote = _musicNotes[songNumber];
        _currentNoteSequence = _musicNotes[songNumber].NoteSequence;
        _currentHighlighters = _musicNotes[songNumber].Hilighters;
        _currentPages = _musicNotes[songNumber].Pages;
        
        RestartNotes();
    }

    private void OnEnable()
    {
        TurnPage.OnTurnComplete += ResumeHilighting;
    }

    private void OnDisable()
    {
        TurnPage.OnTurnComplete -= ResumeHilighting;
    }

    public void ExitPlayingMode()
    {
        _isPaused = true;
        foreach (GameObject highlighter in _currentHighlighters)
        {
            highlighter.SetActive(false);
        }
        foreach (GameObject key in _keys)
        {
            if(key.GetComponent<Key>().child1 != null)
                key.GetComponent<Key>().child1.SetActive(false);
        }
        foreach (GameObject currentPage in _currentPages)
        {
            currentPage.SetActive(false);
        }
    }
}