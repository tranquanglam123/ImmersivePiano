using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class AdjustMenuHandler : MonoBehaviour
{
    [SerializeField] Button showMenuBttn;
    [SerializeField] GameObject pianoSystem;
    private float _offset = 0.05f;
    //private Button _rotateLeft;
    //private Button _rotateRight;
    //private Button _forward;
    //private Button _backward;
    //private Button _left;
    //private Button _right;
    //private Button _down;
    //private Button _up;
    private void Awake()
    {
        if (pianoSystem == null)
        {
            pianoSystem = GameObject.Find("PianoSystem");
        }
        try
        {
            showMenuBttn.onClick.AddListener(delegate { ToggleMenu(); });
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        try
        {
            var canvas = transform.GetChild(0);
            for (int i = 0; i < 9; i++)
            {
                Button bttn = canvas.GetChild(i).gameObject.GetComponent<Button>();
                switch (i)
                {
                    case 0:
                        bttn.onClick.AddListener(delegate {
                            RotateLeft();
                        });
                        break;
                    case 1:
                        bttn.onClick.AddListener(delegate {
                            Forward();
                        });
                        break;
                    case 2:
                        bttn.onClick.AddListener(delegate {
                            RotateRight();
                        });
                        break;
                    case 3:
                        bttn.onClick.AddListener(delegate {
                            Left();
                        });
                        break;
                    case 4:
                        bttn.onClick.AddListener(delegate {
                            ResetPiano();
                        });
                        break;
                    case 5:
                        bttn.onClick.AddListener(delegate {
                            Right();
                        });
                        break;
                    case 6:
                        bttn.onClick.AddListener(delegate {
                            Down();
                        });
                        break;
                    case 7:
                        bttn.onClick.AddListener(delegate {
                            Backward();
                        });
                        break;
                    case 8:
                        bttn.onClick.AddListener(delegate {
                            Up();
                        });
                        break;
                }
        }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    #region Adjust Functions
    public void RotateLeft()
    {
        Quaternion quaternion = pianoSystem.transform.localRotation;
        Quaternion rotOffset = Quaternion.Euler(0, -1, 0);
        pianoSystem.transform.localRotation = quaternion * rotOffset;
        Debug.Log("Rotate Left");
    }
    public void RotateRight()
    {
        Quaternion quaternion = pianoSystem.transform.localRotation;
        Quaternion rotOffset = Quaternion.Euler(0, 1, 0);
        pianoSystem.transform.localRotation = quaternion * rotOffset;
        Debug.Log("Rotate Right");
    }
    public void Forward()
    {
        Vector3 pos = pianoSystem.transform.localPosition;
        pos.z += _offset;
        pianoSystem.transform.localPosition = pos;
        Debug.Log("Forward");
    }
    public void Backward()
    {
        Vector3 pos = pianoSystem.transform.localPosition;
        pos.z -= _offset;
        pianoSystem.transform.localPosition = pos;
        Debug.Log("Backward");
    }
    public void Left()
    {
        Vector3 pos = pianoSystem.transform.localPosition;
        pos.x -= _offset;
        pianoSystem.transform.localPosition = pos;
        Debug.Log("Left");
    }
    public void Right()
    {
        Vector3 pos = pianoSystem.transform.localPosition;
        pos.x += _offset;
        pianoSystem.transform.localPosition = pos;
        Debug.Log("Right");
    }
    public void Up()
    {
        Vector3 pos = pianoSystem.transform.localPosition;
        pos.y += _offset;
        pianoSystem.transform.localPosition = pos;
        Debug.Log("Up");
    }
    public void Down()
    {
        Vector3 pos = pianoSystem.transform.localPosition;
        pos.y -= _offset;
        pianoSystem.transform.localPosition = pos;
        Debug.Log("Down");
    }
    public void ResetPiano()
    {
        //pianoSystem.transform.localPosition = new Vector3(0, 10, 0);
        //pianoSystem.SetActive(false);
        //FindAnyObjectByType<GetPointerHandler>().ResetSetup();
        Debug.Log("Reset Piano");
    }
    public void ToggleMenu()
    {
        bool toggleMenu = gameObject.activeSelf;
        transform.gameObject.SetActive(!toggleMenu);
    }
    #endregion
}
