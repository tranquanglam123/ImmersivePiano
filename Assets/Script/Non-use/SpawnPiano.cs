using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPiano : OVRSceneManager
{
    [HideInInspector]
    public OVRSceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void LoadPiano()
    {
        var classifications = new[]
        {
            OVRSceneManager.Classification.Table
            };
        var test = sceneManager.RequestSceneCapture(classifications);
        while (true)
        {
            if (test) { OverlayPiano();
                break;
            }
        }
    }
    void OverlayPiano()
    {
        //get the table mesh from ovr scene manager
             
    }
        
    
    
}
