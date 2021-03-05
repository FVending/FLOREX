using Boo.Lang;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Click_Button : MonoBehaviour
{
    GameObject Camera;
    string Value;
    string SensorValue;
    string Cell = "";
    string DoubleCell = "";
    bool Price_Bool = true;
    List<string> Price_Value;
  
   

    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera"); // Ищем главную камеру на которой основные скрипты    
    }

    void Read_Price()
    {
        Price_Value = new List<string>(Camera.GetComponent<BasePlayer>().Price);
    }

    void Input_Button()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown("1")) Cell_NUM("1");
            if (Input.GetKeyDown("2")) Cell_NUM("2");
            if (Input.GetKeyDown("3")) Cell_NUM("3");
            if (Input.GetKeyDown("4")) Cell_NUM("4");
            if (Input.GetKeyDown("5")) Cell_NUM("5");
            if (Input.GetKeyDown("6")) Cell_NUM("6");
            if (Input.GetKeyDown("7")) Cell_NUM("7");
            if (Input.GetKeyDown("8")) Cell_NUM("8");
            if (Input.GetKeyDown("9")) Cell_NUM("9");
            if (Input.GetKeyDown("0")) Cell_NUM("0");
        }
    }

    void Cell_NUM(string Numer_Push_Btn)// тут проверяем номер ячейки. если номер из двух цыфр, то норм
    {
        if (DoubleCell.Length < 2) DoubleCell = DoubleCell + Numer_Push_Btn;
        if (DoubleCell.Length == 2)
        {
            foreach (string Val in Price_Value) // Перебераем прайс
            {
                if (Val.Split('=').First() == DoubleCell)
                {
                    Value = Val.Split('=').Last();
                    Cell = DoubleCell;
                    Debug.Log("Цена " + Value + " Ячейка " + Cell);
                    DoubleCell = "";
                    Numer_Push_Btn = "";
                    Debug.Log(DoubleCell.Length);
                    Camera.GetComponent<MDB>().Click_Price(Value, Cell); // обращаемся к функции и передаем значение
                }

            }

        }
    }

    //void OnGUI()
    //{
    //    GUIStyle styleTime = new GUIStyle();
    //    styleTime.fontSize = 80;
    //    styleTime.normal.textColor = Color.red;
    //    if(int.Parse(Cell) < 11 || int.Parse(Cell) > 17)
    //    GUI.Button(new Rect(200, 800, 200, 100), "Нет такой ячейки!", styleTime);        
    //}



    public void Click_Butt(string Sensor_Cell)
    {     
        foreach (string Val in Price_Value) // Перебераем прайс
        {
            if (Val.Split('=').First() == Sensor_Cell)
            {
                SensorValue = Val.Split('=').Last();
                Debug.Log("Цена " + SensorValue + " Ячейка " + Sensor_Cell);
                Camera.GetComponent<MDB>().Click_Price(SensorValue, Sensor_Cell); // обращаемся к функции и передаем значение
            }          
        }
    }


    void Update()
    {
        if (Price_Bool)// тут читаем прайс
        {
            Read_Price();
            if (Price_Value.Count != 0) Price_Bool = false;
        }

        Input_Button();
    }
}
