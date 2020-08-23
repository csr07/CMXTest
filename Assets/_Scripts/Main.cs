using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private GameObject camera1;
    private GameObject mainCameraGO;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraGO = GameObject.FindGameObjectWithTag("MainCamera");
        camera1 = GameObject.Find("Camera1");

        mainCameraGO.SetActive(false);
        camera1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
