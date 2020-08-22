using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{    
    public float[] randomList;

    // Start is called before the first frame update
    void Start()
    {
        randomList = new float[1000000];
        Generate();
    }
    void Generate()
    {
        
        float startTime = Time.time;
        Debug.Log("startTime: " + startTime);

        System.Random rnd = new System.Random();
        for (int i = 0; i < randomList.Length; i++) randomList[i] = (float)rnd.NextDouble();

        float endTime = Time.time;
        float elapsedTime = endTime - startTime;


        Debug.Log("endTime: " + endTime);
        Debug.Log("elapsedTime: " + elapsedTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
