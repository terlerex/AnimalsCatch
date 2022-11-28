using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TraductionXML : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Traduction()
    {
        string key = "trnsl.1.1.20191201T161100Z.0d0f9f9b1b1b1b1b.0d0f9f9b1b1b1b1b";
        string url = "https://translate.yandex.net/api/v1.5/tr/translate?lang=en-fr";
        StartCoroutine(GetRequest(url));
    }
    
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
    
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
    
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}
