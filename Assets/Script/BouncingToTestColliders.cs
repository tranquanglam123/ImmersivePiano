using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingToTestColliders : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chessPiece;
    public float speed;
    public void Start()
    {
        //get the right hand controller

    }

    // Update is called once per frame
    public void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            GameObject spawnChessPiece = Instantiate(chessPiece, transform.position, Quaternion.identity);
            Rigidbody rb = spawnChessPiece.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * speed;
        }
    }
}
