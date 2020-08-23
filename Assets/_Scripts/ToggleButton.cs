using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    Toggle m_Toggle;

    CameraManager cameraMgr;
    public GameObject cameraSlider;

    public int targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();

        //Fetch CameraController script
        cameraMgr = GameObject.FindObjectOfType<CameraManager>();

        cameraSlider.GetComponent<Slider>().interactable = m_Toggle.isOn;

        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });        
    }
    
    void ToggleValueChanged(Toggle change)
    {
        //m_Text.text = "New Value : " + m_Toggle.isOn;
        if (m_Toggle.isOn)
        {
            if (targetCamera <= cameraMgr.cameras.Length)
            {
                cameraMgr.SwapToCamera(targetCamera - 1);
            }            
        }

        cameraSlider.GetComponent<Slider>().interactable = m_Toggle.isOn;
    }
}
