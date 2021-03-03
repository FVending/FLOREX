using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PasswordServise : MonoBehaviour
{

    [SerializeField] InputField PassValue; // Вносим в переменую поле для пароля
    string Pass;
    string Read_Value;
    public GameObject Admin_Panel;
    public GameObject Errors_Panel;


    void Awake()
    {
        if (!File.Exists(Environment.CurrentDirectory + @"\FLOREX_Data\StreamingAssets\Data\Save\ServisePassword.smp"))
        {
            File.Create(Environment.CurrentDirectory + @"\FLOREX_Data\StreamingAssets\Data\Save\ServisePassword.smp");
        }
    }

    void Start()
    {
        StreamReader Read_Password = new StreamReader(Environment.CurrentDirectory + @"\FLOREX_Data\StreamingAssets\Data\Save\ServisePassword.smp"); // Читаем из файла
        Read_Value = Read_Password.ReadLine(); // Читаем сам пароль
    }

  
    void Update()
    {
        
    }

    public void Reset_Pass()
    {
        PassValue.text = null;
        Pass = null;
    }

    public void SetPassword(string Symvol) // Получаем значение с кнопок
    {
        if(PassValue.text.Length != 8) // Сравниваем на кол-во символов
        {
            Pass = Pass + Symvol; // Берем симвог и плюсуем к переменой
            PassValue.text = Pass; // Запись переменой в поле для пароля
        }
        if (PassValue.text.Length == 8) // Если символов = 8
        {
          
            if (String.Equals(Read_Value, Pass)) // Сравниваем то что ввкли в поле и пароль
            {
                gameObject.SetActive(false);
                Admin_Panel.SetActive(true);

            }
            else
            {
                gameObject.SetActive(false);
                Errors_Panel.SetActive(true);
                //Errors_Panel.transform.SetParent(transform);
                Errors_Panel.GetComponent<Errors>().ERROR("НЕ ВЕРНЫЙ ПАРОЛЬ!");
  
            }
        }
    }


}
