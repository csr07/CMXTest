using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCB : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;        
        playerTransform = GameObject.Find("targetLookAt").transform;        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTransform);
    }
}
