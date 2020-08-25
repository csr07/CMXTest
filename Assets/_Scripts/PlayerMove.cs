using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    //private float speed = 3.0f;
    public float horizontalInput;

    private float forwardSpeed = 7.0f;
    private float rotateSpeed = 2.0f;

    private Vector3 velocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        //float hmovement = Input.GetAxis("Horizontal");
        //float vmovement = Input.GetAxis("Vertical");
        //GetComponent<Rigidbody>().velocity = new Vector3(hmovement * speed, GetComponent<Rigidbody>().velocity.y, vmovement * speed);

        //Quick Android input test
        //if (Input.touchCount > 0 )
        //{
        //    switch (Input.touches[0].phase)
        //    {
        //        //case TouchPhase.Began:
        //        //    prePosX = Input.touches[0].po
        //        //    break;
        //        case TouchPhase.Moved:

        //            hmovement = Input.touches[0].deltaPosition.x;
        //            vmovement = Input.touches[0].deltaPosition.y;
        //            GetComponent<Rigidbody>().velocity = new Vector3(hmovement * 3, 0.0f, vmovement * 3);
        //            break;
        //    }
        //}

        float v = 1.0f; //forward positive velocity
        float h = horizontalInput; //simulating value for horizontal movement;

        velocity = new Vector3(0, 0, v);

        velocity = transform.TransformDirection(velocity);

        velocity *= forwardSpeed;

        transform.localPosition += velocity * Time.fixedDeltaTime;

        transform.Rotate(0, h * rotateSpeed, 0);
    }
}
