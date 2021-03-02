using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Errors : MonoBehaviour
{
    float nextTime = 0.0F;
    float timeRate = 5F;
    bool StartTime = false;
    GameObject Start_Panel;

    void Start()
    {
        //gameObject.SetActive(false);
        Start_Panel = GameObject.Find("Panel_Price");
    }

    public void ERROR(string Error_Val)
    {
        gameObject.SetActive(true);
        nextTime = Time.time + timeRate;
        gameObject.transform.GetChild(1).GetComponent<Text>().text = Error_Val; // Выводим текст ошибки из переменной Error_Val
        StartTime = true;
        //Debug.Log("ОШИБКА");
    }

  
    void Update()
    {
        if (StartTime)
        {
            Start_Panel.SetActive(false);
            if (Time.time > nextTime)
            {
                nextTime = Time.time + timeRate;
                gameObject.SetActive(false);
                StartTime = false;
                Start_Panel.SetActive(true);
            }
        }
    }

}
