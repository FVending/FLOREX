using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float nextTime = 0.0F;
    public float timeRate = 5F;
    bool StartTime = false;
    bool Active_Object = true;

    void Start()
    {
        
    }

 
    void Update()
    {

        if (StartTime)
        {
            Active_Object = false;

            if (Time.time > nextTime)
            {
                nextTime = Time.time + timeRate;
                gameObject.SetActive(false);
                StartTime = false;
                Active_Object = true;
                //Debug.Log("!!!!!!");
            }
        }
        if (Active_Object)
        {
            if (gameObject.activeSelf)
            {
                nextTime = Time.time + timeRate;
                StartTime = true;
                
            }
        }

    }
}
