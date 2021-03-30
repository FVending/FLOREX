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
    public GameObject Cell_Price_Window;
    public string Cell_For_New_Price;

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

    public void Installation_Cell(bool Service_And_Shop)
    {
        Cell_Price_Window.SetActive(Service_And_Shop);
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

  
        ReadPrice.Close();
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();// Выход из игры
    }
}
