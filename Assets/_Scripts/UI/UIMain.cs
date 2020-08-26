using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    public GameObject[] mToggleGO;
    public GameObject[] mSliderGO;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableToggleInteraction(int numToggle, bool value)
    {
        mToggleGO[numToggle - 1].GetComponent<Toggle>().isOn = value;
    }

    public void EnableSliderInteraction(int numSlider, bool value)
    {
        mSliderGO[numSlider - 1].GetComponent<Slider>().interactable = value;
    }    
}
