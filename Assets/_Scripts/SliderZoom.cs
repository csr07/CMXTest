using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderZoom : MonoBehaviour
{
    Slider m_slider;
    public GameObject m_camera;

    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();

        m_slider.onValueChanged.AddListener(delegate {
            ValueChangeCheck();
        });        
    }

    public void ValueChangeCheck()
    {
        Camera camCmp = m_camera.GetComponent<Camera>();
        camCmp.fieldOfView = m_slider.value;
    }
}
