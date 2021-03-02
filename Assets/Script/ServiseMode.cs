using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiseMode : MonoBehaviour
{
    float nextTime = 0.0F;
    float timeRate = 5F;
    bool StartTime = false;
    public GameObject Start_Panel;
    public GameObject Servise_Panel;

    void Start()
    {
        //Servise_Panel.SetActive(false);
    }

    

    void Update()
    {

        if (!Servise_Panel.activeSelf)
        {           
            Servise_Panel.GetComponent<PasswordServise>().Reset_Pass();            
        }

        if (StartTime)
        {
            
            if (Time.time > nextTime)
            {
                nextTime = Time.time + timeRate;
                Start_Panel.SetActive(false);
                Servise_Panel.SetActive(true);
                StartTime = false;
            }           
        }
        else
        {
            nextTime = 0.0F;
        }

    }

    public void Push_Button(bool PUSH = false)
    {
        StartTime = PUSH;
        nextTime = Time.time + timeRate;
        //Debug.Log(PUSH);
    }
}
