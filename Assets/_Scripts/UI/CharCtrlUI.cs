using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharCtrlUI : MonoBehaviour
{
    Toggle mToggle;    

    // Start is called before the first frame update
    void Start()
    {
        mToggle = GetComponent<Toggle>();

        //Add listener for when the state of the Toggle changes, to take action
        mToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }

    void ToggleValueChanged()
    {
        TP_Controller.Instance.config_ManualPlayer = mToggle.isOn;
    }
}
