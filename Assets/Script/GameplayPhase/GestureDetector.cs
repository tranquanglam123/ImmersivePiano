using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized; 
}

public class GestureDetector : MonoBehaviour
{
    public float threshhold = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public bool debugMode = true;
    //private List<OVRBone> fingerBones;
    private Gesture previousGesture;
    public static string rightHandGesture;
    public static string leftHandGesture;

    // Start is called before the first frame update
    void Start()
    {
       // fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
    }

    // Update is called once per frame
    void Update()
    {
        if (debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }

        Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());
        if(hasRecognized && !currentGesture.Equals(previousGesture))
        {
            Debug.Log("New Gesture Found: " + currentGesture.name);
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
        }

        if (currentGesture.name == "Left")
            leftHandGesture = currentGesture.name;
        else
            rightHandGesture = currentGesture.name;
        //Debug.Log("New Gesture Found: " + currentGesture.name);
    }

    void Save()
    {
        Gesture g = new Gesture();
        g.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach(var bone in skeleton.Bones)
        {
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }
        g.fingerDatas = data;
        gestures.Add(g);
    }

    Gesture Recognize()
    {
        Gesture currentGesture = new Gesture();
        float currentMin = Mathf.Infinity;

        foreach(var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscaarded = false;
            for (int i = 0; i < skeleton.Bones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(skeleton.Bones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);

                if(distance > threshhold)
                {
                    isDiscaarded = true;
                    break;
                }

                sumDistance += distance;
            }

            if(!isDiscaarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentGesture = gesture;
            }
        }

        return currentGesture;
    }
}
