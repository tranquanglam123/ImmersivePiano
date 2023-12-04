using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private KeyCode _keyPressed;
    [SerializeField] private NoteHighlighterManager _noteHighlighterManager;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _noteHighlighterManager.SetNextKey();        
        }
    }
}
