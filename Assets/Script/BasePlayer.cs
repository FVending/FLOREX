using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayer : MonoBehaviour
{
    public string NameButton;
    public string ValuePrice;
    public List<string> Price = new List<string>();
    public Transform Parent;
    public Transform Button_Start;
    public GameObject ButtonPrifab;
    GameObject Button;
    int Step = 50;

    void Awake()
    {
        if (!File.Exists(Environment.CurrentDirectory + @"\FLOREX_Data\StreamingAssets\Data\Save\Price.smp"))
        {         
            File.Create(Environment.CurrentDirectory + @"\FLOREX_Data\StreamingAssets\Data\Save\Price.smp");
        }
       
    }

    void Start()
    {
        PriceList();
    }


    void PriceList()
    {
        StreamReader ReadPrice = new StreamReader(Environment.CurrentDirectory + @"\FLOREX_Data\StreamingAssets\Data\Save\Price.smp"); // Читаем сохранение

        if (ReadPrice != null)
        {
            while (!ReadPrice.EndOfStream)
            {
                Price.Add(ReadPrice.ReadLine());

            }

        }

        float HeightContent = Price.Count * Price.Count;
        RectTransform rectTrans = Parent.transform as RectTransform;
        rectTrans.offsetMin = new Vector2(0, - HeightContent);
        string NameFile;

        for (int i = 0; Price.Count > i; i++)
        {
            Button = Instantiate(ButtonPrifab, Button_Start.transform);
            NameFile = Price[i]; //Берем из списка прайса для добавление к кнопке
            NameButton = NameFile.Split('=').First(); //Берем название до равно
            ValuePrice = NameFile.Split('=').Last();  //Берем название после равно
            //Debug.Log(NameButton);
            Button.GetComponentInChildren<Text>().text = NameButton; //Запись названия в кнопку
            Button.GetComponentInChildren<Transform>().GetChild(1).GetComponent<Text>().text = ValuePrice;
            //Button.GetComponentInChildren<Text>().text = Price[i];
            Button.transform.parent = Parent.transform;
            Button.transform.position = new Vector3(Button_Start.position.x, Button_Start.position.y - Step);
            Step = Step + 50;
        }
        ReadPrice.Close();
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();// Выход из игры
    }
}
