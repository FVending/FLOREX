using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float nextTime = 0.0F;
    float timeRate = 1F;
    bool StartTime = false;
    string Swich_On = "/start";
    //string Swich_Off = "/off1";
    string Adress_Controller;

    void Start()
    {
        //gameObject.GetComponent<HttpReqoestExample>().Send("http://192.168.0.10/start");
    }

   
    void Update()
    {
        //if (StartTime)
        //{
           
        //    if (Time.time > nextTime)
        //    {
        //        nextTime = Time.time + timeRate;
              
        //        StartTime = false;
        //        gameObject.GetComponent<HttpReqoestExample>().Send(Adress_Controller + Swich_Off);
        //    }
        //}
    }

    public void Magic(string ADR)
    {
        Adress_Controller = ADR;
        nextTime = Time.time + timeRate;
        //gameObject.GetComponent<HttpReqoestExample>().Send(ADR + Swich_On); // рабочий вариант, раскоментировать после тестов
        gameObject.GetComponent<HttpReqoestExample>().Send("http://192.168.4.1/start");// тест, на прямую в контроллер
        StartTime = true;
    }
}
