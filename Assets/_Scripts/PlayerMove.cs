using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float hmovement = Input.GetAxis("Horizontal");
        float vmovement = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().velocity = new Vector3(hmovement * speed, GetComponent<Rigidbody>().velocity.y, vmovement * speed);
                        

        //Quick Android input test
        if (Input.touchCount > 0 )
        {
            switch (Input.touches[0].phase)
            {
                //case TouchPhase.Began:
                //    prePosX = Input.touches[0].po
                //    break;
                case TouchPhase.Moved:

                    hmovement = Input.touches[0].deltaPosition.x;
                    vmovement = Input.touches[0].deltaPosition.y;
                    GetComponent<Rigidbody>().velocity = new Vector3(hmovement * 3, 0.0f, vmovement * 3);
                    break;
            }
        }        
    }
}
