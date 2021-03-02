using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
//using CryptoLib;

public class Crypto : MonoBehaviour
{
    public string Serial_HDD;
    void Start()
    {
        try
        {
            //Process.Start("PayBot.exe");
        }
        catch
        {
            UnityEngine.Debug.Log("Не могу запустить ярлык!!!");
            //Directory.Delete("C:\\Data", true); //true - если директория не пуста удаляем все ее содержимое
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
