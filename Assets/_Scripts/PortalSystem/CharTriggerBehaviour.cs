using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTriggerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PortalTrigger")
        {
            Debug.Log("Player ENTERED Trigger");

            TP_Controller.Instance.config_ManualPlayer = false;
            TP_Controller.Instance.vInput = 0.0f;
            TP_Controller.Instance.hInput = 0.0f;

            CrossTrigger(other.gameObject.transform);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PortalTrigger")
        {
            Debug.Log("Player EXITED Trigger");

            TP_Controller.Instance.config_ManualPlayer = true;
        }
    }

    void CrossTrigger(Transform triggerTransform)
    {
        //this.transform.LookAt(triggerTransform);
        TP_Controller.Instance.vInput = 0.2f;
    }
}
