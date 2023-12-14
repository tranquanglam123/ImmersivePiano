using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] GameObject intro;
    [SerializeField] GameObject edgeConfirm;
    //[SerializeField] GameObject resetEdge;
    [SerializeField] GameObject songsSelection;
    [SerializeField] GameObject inGameOption;
    private int index;

    private void Start()
    {
        index = 0;
        MenuLogic();
    }

    //Menu Logic
    private void MenuLogic()
    {
        switch(index)
        {
            case 0:
                intro.SetActive(true);
                edgeConfirm.SetActive(false);
                //resetEdge.SetActive(false);
                songsSelection.SetActive(false);
                inGameOption.SetActive(false);
                break;
            case 1:
                intro.SetActive(false);
                edgeConfirm.SetActive(true);
                //resetEdge.SetActive(false);
                songsSelection.SetActive(false);
                inGameOption.SetActive(false);
                break;
            case 2:
                break;
            case 3:
                intro.SetActive(false);
                edgeConfirm.SetActive(false);
                //resetEdge.SetActive(true);
                StartCoroutine(AsyncWithPianoSpawn());
                inGameOption.SetActive(false);
                break;
            case 4:
                intro.SetActive(false);
                edgeConfirm.SetActive(false);
                //resetEdge.SetActive(false);
                songsSelection.SetActive(false);
                inGameOption.SetActive(true);
                break;
        }

    }

    public void OnNextButtonPressed()
    {
        index++;
        MenuLogic();
    }
    public void OnBackButtonPressed()
    {
        index--;
        MenuLogic();
    }
    //since piano only spawn after get 2 pointers and after 1 seconds, this menu should also wait for 1 seconds
    IEnumerator AsyncWithPianoSpawn()
    {
        yield return new WaitForSeconds(1);
        songsSelection.SetActive(true);
    }
}
