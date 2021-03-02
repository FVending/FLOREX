using SimpleWifi;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectWiFi : MonoBehaviour
{

    private static string WiFi_Name = "Bisv 92";
    private static Wifi wifi;
    List<string> Net_WiFi = new List<string>();// Создаем пустой список
    private static string Password = "9219000045";
    private static int Index_Count = 0;


    void Awake()
    {
        wifi = new Wifi();

        if (wifi.ConnectionStatus.ToString() == "Disconnected")
        {
            List();
            Connect();
        }
        else
        {
            //Console.Write(accessPoints);
        }

    }

    private static IEnumerable<AccessPoint> List()
    {
        IEnumerable<AccessPoint> accessPoints = wifi.GetAccessPoints().OrderByDescending(ap => ap.SignalStrength);
        int i = 0;
        foreach (AccessPoint ap in accessPoints)
        {
            //Console.WriteLine("{0}. {1} {2}% Connected: {3}", i++, ap.Name, ap.SignalStrength, ap.IsConnected);

            if (ap.Name == WiFi_Name)
            {
                Index_Count = i;
            }
            i++;
            //Console.WriteLine(ap.Name);
        }
        return accessPoints;
    }

    private static void Connect()
    {
        var accessPoints = List(); // записываем список сетей в переменную
        AccessPoint selectedAP = accessPoints.ToList()[Index_Count]; // записываем в переменную нужную нам сеть, индекс был учтановлен ранее
        AuthRequest authRequest = new AuthRequest(selectedAP); // создаем переменную для идентивикации (пароля)
        bool overwrite = true;

        if (authRequest.IsPasswordRequired)
        {
            if (selectedAP.HasProfile) // если профиль уже существует
            {
                overwrite = false;
            }
            if (overwrite)
            {
                if (authRequest.IsUsernameRequired)
                {
                    authRequest.Username = WiFi_Name;
                }

                authRequest.Password = PasswordPrompt(selectedAP);

                if (authRequest.IsDomainSupported)
                {
                    //Console.Write("\r\nPlease enter a domain: ");
                    authRequest.Domain = Password;
                }
            }
        }
        selectedAP.ConnectAsync(authRequest, overwrite, OnConnectedComplete);
    }

    private static string PasswordPrompt(AccessPoint selectedAP)
    {
        string password = string.Empty;

        bool validPassFormat = false;

        while (!validPassFormat)
        {
            //Console.Write("\r\nPlease enter the wifi password: ");
            password = Password;

            validPassFormat = selectedAP.IsValidPassword(password);

            //if (!validPassFormat)
                //Console.WriteLine("\r\nPassword is not valid for this network type.");
        }

        return password;
    }
    private static void OnConnectedComplete(bool success)
    {
       
        Debug.Log("\nOnConnectedComplete, success: {0} !!!!!!!!!!");
    }

    void Update()
    {

    }
}
