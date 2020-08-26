using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    //private float speed = 3.0f;
    //public float horizontalInput;

    //private float forwardSpeed = 7.0f;
    //private float rotateSpeed = 2.0f;

    //private Vector3 AIMoveVector;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!TP_Controller.Instance.config_ManualPlayer)
        { 
            TP_Controller.Instance.vInput = 1.0f;

            if (TP_Controller.Instance.isInSafeArea)
            {
                TP_Controller.Instance.hInput = 0.0f;
            }
            else 
            {                
                TP_Controller.Instance.hInput = TP_Controller.Instance.randomSide;
            }
        }
    }
}