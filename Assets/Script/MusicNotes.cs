using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNotes : MonoBehaviour
{
    [SerializeField] private Songs _song;
    public GameObject pageTurner;
    
    public List<GameObject> Pages { get; private set; } = new List<GameObject>();
    public List<GameObject> Hilighters { get; private set; } = new List<GameObject>();
    public string[] NoteSequence { get; private set; }
    public int[] PageSeparatorsByNote;

    private const int CHILD_PAGES = 0;
    private const int CHILD_HILIGHTERS = 1;


    // Start is called before the first frame update
    void Start()
    {
        GetPages();
        GetHilighters();
        GetNoteSequence();
        DeactivatePagesAndHighlighers();
        pageTurner.SetActive(false);
    }

    public void DeactivatePagesAndHighlighers()
    {
        foreach (GameObject page in Pages)
        {
            page.SetActive(false);
        }

        foreach (GameObject hilighter in Hilighters)
        {
            hilighter.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetPages()
    {
        Transform childPages = transform.GetChild(CHILD_PAGES);
        Debug.Log(childPages.GetChild(0).name);
        
        foreach (Transform child in childPages)
        {
            Pages.Add(child.gameObject);
        }
    }
    
    private void GetHilighters()
    {
        Transform childHilighters = transform.GetChild(CHILD_HILIGHTERS);
        
        foreach (Transform child in childHilighters)
        {
            Hilighters.Add(child.gameObject);
        }
    }

    private void GetNoteSequence()
    {
        NoteSequence = Music.GetNotes(_song);
    }
}
