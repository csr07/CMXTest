using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    Toggle mToggle;
    CameraManager cameraMgr;    
    public int targetCamera;

    // Start is called before the first frame update
    void Start()
    {        
        mToggle = GetComponent<Toggle>();     
        cameraMgr = GameObject.FindObjectOfType<CameraManager>();        


        //Add listener for when the state of the Toggle changes, to take action
        mToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });        
    }
    
    void ToggleValueChanged()
    {           
        cameraMgr.SetToggleForCamera(targetCamera, mToggle.isOn);        
    }
}
