using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleWifi;
using System.Diagnostics;
using System;

public class WiFi : MonoBehaviour
{
   
    void Start()
    {
        Process.Start(Environment.CurrentDirectory + @"\PayBot_Data\StreamingAssets\Data\Save\WiFi_Connect.exe");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
