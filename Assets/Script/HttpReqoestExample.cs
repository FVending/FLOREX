using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpReqoestExample : MonoBehaviour
{
 
    private string InfoPin = "http://192.168.4.1";
    public bool Shop = false;

    void Start()
    {     
    }
    public void Send(string Adress, bool sh)
    {
        StartCoroutine(SendRequest(Adress));
        Shop = sh;
        //gameObject.GetComponent<BasePlayer>().Installation_Cell(true);// установка букета подгон ячейки
    }

 
    public IEnumerator SendRequest(string URL)
    {   
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();
        URL = "";
        //Debug.Log(request.downloadHandler.text);
        //Debug.Log(URL);
    }

    private IEnumerator SendRequest2()
    {        
        UnityWebRequest request = UnityWebRequest.Get(this.InfoPin);

        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);

        if(request.downloadHandler.text == "DONE")
        {
            if (Shop)
            {
                gameObject.GetComponent<BasePlayer>().Installation_Cell(false);// Продажа букета подгон ячейки
            }
            else
            {
                gameObject.GetComponent<BasePlayer>().Installation_Cell(true);// установка букета подгон ячейки
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Shop)
            {
                gameObject.GetComponent<BasePlayer>().Installation_Cell(false);// Продажа букета подгон ячейки
            }
            else
            {
                gameObject.GetComponent<BasePlayer>().Installation_Cell(true);// установка букета подгон ячейки
            }
        }
        //StartCoroutine(SendRequest2());
    }
}
