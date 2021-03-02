using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    [SerializeField] InputField Num_Mashine; // Вносим в переменую поле для номера
    [SerializeField] InputField WiFi_Name; // Вносим в переменую поле для имени вайфай
    [SerializeField] InputField WiFi_Password; // Вносим в переменую поле для пароля вайфай
    string Change_Num; // Переменная для проверки изменений в поле
    string Change_WiFi_Name; // Переменная для проверки изменений в поле
    string Change_WiFi_Password; // Переменная для проверки изменений в поле
    //StreamReader ReadConfig = new StreamReader("Data/Save/Config.smp"); // Читаем сохранение
    string[] ReadConfig = File.ReadAllLines(Environment.CurrentDirectory + @"\PayBot_Data\StreamingAssets\Data\Save\Config.smp"); // Создаем массив в котором будут строки из файла в корневой дерриктории проекта
    List<string> Config_Lict = new List<string>();// Создаем пустой список
    int fix = 0;
    int Fix_WiFi_Name = 0;
    int Fix_WiFi_Password = 0;
    bool Write_True = false;

    public GameObject Errors_Panel;

    void Awake()
    {
      
    }
    void Start()
    {
        //gameObject.SetActive(false); // Закрываем страницу админа

        if (!File.Exists(Environment.CurrentDirectory + @"\PayBot_Data\StreamingAssets\Data\Save\Config.smp"))
        {
            Errors_Panel.SetActive(true);
            Errors_Panel.GetComponent<Errors>().ERROR("Не найден Config.smp");    
        }
        else
        {
            int count = 0;

            foreach (string s in ReadConfig) // перебираем массив
            {
                Config_Lict.Add(s); // добовляем строкм из массива в список
                
                if (s.Split('=').First() == "name") // если в массиве нашли перед символом равно (name)
                {
                    fix = count; // запоминаем какой индекс
                    Write_True = true; // разрешаем перезаписать файл
                    Num_Mashine.text = s.Split('=').Last(); // записываем имя в поле
                    Change_Num = Num_Mashine.text; // запись поля в переменную
                }
                if (s.Split('=').First() == "WiFi_Name") // если в массиве нашли перед символом равно (WiFi_Name)
                {
                    Fix_WiFi_Name = count; // запоминаем какой индекс
                    Write_True = true; // разрешаем перезаписать файл
                    WiFi_Name.text = s.Split('=').Last(); // записываем имя в поле
                    Change_WiFi_Name = WiFi_Name.text; // запись поля в переменную
                }
                if (s.Split('=').First() == "WiFi_Password") // если в массиве нашли перед символом равно (WiFi_Password)
                {
                    Fix_WiFi_Password = count; // запоминаем какой индекс
                    Write_True = true; // разрешаем перезаписать файл
                    WiFi_Password.text = s.Split('=').Last(); // записываем имя в поле
                    Change_WiFi_Password = WiFi_Password.text; // запись поля в переменную
                }

                count = count + 1;
            }

        }
        

    }

    public void Write_Conf()
    {
        Config_Lict.RemoveAt(fix); // удаляем индекс
        Config_Lict.Insert(fix, "name=" + Num_Mashine.text); // записываем на его место готоаое название ком порта
        Config_Lict.RemoveAt(Fix_WiFi_Name); // удаляем индекс
        Config_Lict.Insert(Fix_WiFi_Name, "WiFi_Name=" + WiFi_Name.text); // записываем на его место готоаое название ком порта
        Config_Lict.RemoveAt(Fix_WiFi_Password); // удаляем индекс
        Config_Lict.Insert(Fix_WiFi_Password, "WiFi_Password=" + WiFi_Password.text); // записываем на его место готоаое название ком порта

        if (Write_True)
        {
            StreamWriter Write_Config = new StreamWriter(Environment.CurrentDirectory + @"\PayBot_Data\StreamingAssets\Data\Save\Config.smp");// C:\Users\ser\Desktop\PayBotGame\PayBot_Data
            foreach (string r in Config_Lict) // запускаем цикл перезаписи
            {
                Write_Config.WriteLine(r);
            }
            Write_Config.Close();
            Change_Num = Num_Mashine.text;
            Change_WiFi_Name = WiFi_Name.text;
            Change_WiFi_Password = WiFi_Password.text;
        }
 
    }

    void Update()
    {
        if (Num_Mashine.text != Change_Num)
        {
            Write_Conf();
        }
        if (WiFi_Name.text != Change_WiFi_Name)
        {
            Write_Conf();
        }
        if (WiFi_Password.text != Change_WiFi_Password)
        {
            Write_Conf();
        }
    }


}
