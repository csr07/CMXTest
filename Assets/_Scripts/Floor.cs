using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject mPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player INSIDE");
            if (mPlayer)
            {
                //TP_Controller.Instance.hInput = 0.0f;
                TP_Controller.Instance.isInSafeArea = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player OUTSIDE");
            if (mPlayer)
            {                
                //TP_Controller.Instance.hInput = 1.0f;
                TP_Controller.Instance.isInSafeArea = false;

                if (Random.Range(-1.0f, 1.0f) >= 0.0f)
                {
                    TP_Controller.Instance.randomSide = 1.0f;
                    //Debug.Log("turn RIGHT");
                }
                else
                {
                    TP_Controller.Instance.randomSide = -1.0f;
                    //Debug.Log("turn LEFT");
                }

                //reduce Health Test
                mPlayer.GetComponent<PlayerInfo>().playerStats.health -= 10;

            }
        }
    }
}
