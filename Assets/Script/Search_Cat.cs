using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Search_Cat : MonoBehaviour
{
    string[] allDrives = Environment.GetLogicalDrives();
    int Drivers_Length;

    void Start()
    {
        foreach (string s in allDrives)
        {
            Drivers_Length = allDrives.Length;
        }
    }

   
    void Update()
    {
        
    }

    public void Search_New_Drivers()
    {
        DriveInfo[] drives = DriveInfo.GetDrives(); // Список дисков и флешек
        string[] Files = Directory.GetFiles(drives[drives.Length - 1].ToString()); // Список файлов на диске

        foreach (DriveInfo Fleshka in drives) // Ищем флешку из списка дисков
        {
            if (Fleshka.IsReady && (Fleshka.DriveType == DriveType.Removable)) // Усли флешка вставлена и ее тип Removable
            {
                //Debug.Log(Fleshka);
                foreach (string dir in Files) // Ищем нужный файл
                {
                    if (dir == Fleshka + @"Price.smp") // Если файл найден
                    {
                        string fileOne = dir; // Записываем файл в переменную
                        //Debug.Log(fileOne);
                        File.Delete(Environment.CurrentDirectory + @"\PayBot_Data\StreamingAssets\Data\Save\Price.smp"); // Удаляем файл из деректории с игрой
                        File.Copy(fileOne, Environment.CurrentDirectory + @"\PayBot_Data\StreamingAssets\Data\Save\Price.smp"); // Записываем новый в деректорию с игрой
                        SceneManager.LoadScene("PayBot"); // Перезапуск программы
                    }
                }                
            }
        }
    }
}
