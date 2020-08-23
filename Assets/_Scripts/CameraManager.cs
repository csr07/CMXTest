using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cameras;
    private int currentCameraIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentCameraIndex = 0;

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            Debug.Log("currentCamera: " + currentCameraIndex);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //swap to next camera on array
            SwapToCamera(currentCameraIndex + 1);
        }
    }    

    public void SwapToCamera(int cameraIndex)
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);

        cameraIndex %= cameras.Length;

        currentCameraIndex = cameraIndex;

        cameras[currentCameraIndex].gameObject.SetActive(true);

        Debug.Log("currentCamera: " + currentCameraIndex);
    }
}
