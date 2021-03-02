using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_Button : MonoBehaviour
{
    GameObject Camera;
    string Value;
    string Name;
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera"); // Ищем главную камеру на которой основные скрипты
    }

    public void Click_Butt()
    {
        Value = gameObject.GetComponentInChildren<Transform>().GetChild(1).GetComponent<Text>().text; // присваиваем значение цены
        Name = gameObject.GetComponentInChildren<Transform>().GetChild(0).GetComponent<Text>().text; // присваиваем имя
        Camera.GetComponent<MDB>().Click_Price(Value, Name); // обращаемся к функции и передаем значение
    }


    void Update()
    {
        
    }
}
