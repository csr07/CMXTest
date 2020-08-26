using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cameras;
    private int currentCameraIndex;

    private UIMain uiMain;

    // Start is called before the first frame update
    void Start()
    {
        uiMain = FindObjectOfType<UIMain>();

        currentCameraIndex = 0;        

        for (int i = 0; i < cameras.Length; i++)
        {
            SetToggleForCamera(i + 1, currentCameraIndex == i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //swap to next camera on array
            SwapToCamera(currentCameraIndex + 1); //to the +1 camera in array
        }
    }    

    public void SwapToCamera(int newCameraIndex)
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);

        newCameraIndex %= cameras.Length;

        SetToggleForCamera(newCameraIndex + 1, true);

        //Debug.Log("currentCamera: " + (currentCameraIndex+1));
    }

    public void SetToggleForCamera(int camNum, bool value)
    {
        cameras[camNum - 1].gameObject.SetActive(value); //swaps the camera in the scene

        if (value) //on
        {
            currentCameraIndex = camNum - 1;
        }

        //Update UI
        EnableToggleUI(camNum, value);
        EnableSliderUI(camNum, value);
    }

    public void SetFOVForCamera(int camNum, float value)
    {
        cameras[camNum - 1].GetComponent<Camera>().fieldOfView = 100 - value;
    }

    public void EnableToggleUI(int camNum, bool enable)
    {
        //Debug.Log("EnableToggleUI num: " + camNum);
        uiMain.EnableToggleInteraction(camNum, enable);
    }

    public void EnableSliderUI(int camNum, bool enable)
    {
        //Debug.Log("EnableSliderUI num: " + camNum);
        uiMain.EnableSliderInteraction(camNum, enable);
    }
}
