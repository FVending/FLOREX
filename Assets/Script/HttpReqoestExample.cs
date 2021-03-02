using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpReqoestExample : MonoBehaviour
{
    /* [SerializeField]*/
    //private string url_On_1 = "http://192.168.0.10/on1";
    //private string url_Off_1 = "http://192.168.0.10/off1";
    private string InfoPin = "http://192.168.0.10";


    void Start()
    {
        //StartCoroutine(SendRequest("http://192.168.0.10/start"));
    }
    public void Send(string Adress)
    {
        StartCoroutine(SendRequest(Adress));
    }


    public IEnumerator SendRequest(string URL)
    {
   
        UnityWebRequest request = UnityWebRequest.Get(URL);

        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
    }

    private IEnumerator SendRequest2()
    {
        
        UnityWebRequest request = UnityWebRequest.Get(this.InfoPin);

        yield return request.SendWebRequest();
        Debug.Log(request.responseCode);
    }

    void Update()
    {
        //StartCoroutine(SendRequest2());
    }
}
