using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Cost_Cell : MonoBehaviour
{
    [SerializeField] InputField In_Value; // Вносим в переменую поле для стоимости
    string Value;
    string Old_Cell;
    List<string> New_Price = new List<string>();// Создаем пустой список
    GameObject Camera;

    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera"); // Ищем главную камеру на которой основные скрипты    
    }

    public void Reset_Cost()
    {
        In_Value.text = null;
        Value = null;
    }
    public void Cost_Val(string Symvol)
    {
        Value = Value + Symvol; // Берем симвог и плюсуем к переменой
        In_Value.text = Value; // Запись переменой в поле для стоимости
    }
    public void Enter_Value()
    {
        int index_old = 0;
        int index_new = 0;
        string For_Change_Price;

        if (Value != null)
        {
            PriceList(); // вызываем прайс
            Old_Cell = Camera.GetComponent<BasePlayer>().Cell_For_New_Price;// получаем номер ячейки

            foreach (string i in New_Price)// переберем прайс
            {
                if (i.Split('=').First() == Old_Cell)
                {
                    index_new = index_old;// запоминаем индекс найденого          
                }
                index_old++;             
            }
            For_Change_Price = New_Price[index_new]; // присваиваем значение в найденном индаксе
            New_Price[index_new] = New_Price[index_new].Replace(For_Change_Price, Old_Cell + "=" + Value);// меняем всю строку на новую
            //string result = string.Join(",", New_Price);
            //Debug.Log(result);
            Write_Price();
        }
        Reset_Cost();
        gameObject.SetActive(false);// гасим окно для ввода цены
    }

    void PriceList()
    {
        StreamReader ReadPrice = new StreamReader(Environment.CurrentDirectory + @"\FLOREX_Data\StreamingAssets\Data\Save\Price.smp"); // Читаем сохранение
        New_Price.Clear();// очистка списка
        if (ReadPrice != null)
        {
            while (!ReadPrice.EndOfStream)
            {
                New_Price.Add(ReadPrice.ReadLine());
            }
        }
        ReadPrice.Close();
    }
    void Write_Price() // перезаписыаем прайс с новым списком
    {
        StreamWriter WritePrice = new StreamWriter(Environment.CurrentDirectory + @"\FLOREX_Data\StreamingAssets\Data\Save\Price.smp");//, true, System.Text.Encoding.Default

        foreach (string i in New_Price)// переберем прайс
        {
            WritePrice.WriteLine(i);
            //Debug.Log(i);
        }
        WritePrice.Close();
    }

    void Update()
    {

    }
}
