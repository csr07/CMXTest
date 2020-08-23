using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderZoom : MonoBehaviour
{
    Slider m_slider;    
    CameraManager cameraMgr;
    public int mBoundedCamera;

    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
        cameraMgr = GameObject.FindObjectOfType<CameraManager>();

        //Add listener for when the state of the Slider changes, to take action
        m_slider.onValueChanged.AddListener(delegate {
            ValueChangeCheck();
        });        
    }

    public void ValueChangeCheck()
    {
        cameraMgr.SetFOVForCamera(mBoundedCamera, m_slider.value); 
    }
}
