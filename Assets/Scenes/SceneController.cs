using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public static string keyWord;
    void Start()
    {
        //StartCoroutine(GetRequest("https://bit.ly/3iSheyg"));
       // StartCoroutine(GetRequest());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();

           
    }
    public void GoNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void KeyWordForGoogle(string keyWordName)
    {
        keyWord = keyWordName;
        Debug.Log(keyWord);
        //string url = "https://bit.ly/3iSheyg";
        // StartCoroutine(GetRequest(url));
        //StartCoroutine(GetRequest());
    }
    IEnumerator GetRequest()
    {
        string uri = "https://bit.ly/3iSheyg";
        UnityWebRequest unityWeb = UnityWebRequest.Get(uri);
        Debug.Log("unityWeb: " + unityWeb.result);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
           
            // Request and wait for the desired page.
            Debug.Log("In Coroutine Function");
            yield return webRequest.SendWebRequest();
           // Debug.Log(webRequest.result);
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
