using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Scenes;

public class Post : MonoBehaviour
{
    [SerializeField] private string url;
    void Start()
    {
        StartCoroutine(SendRequest());
    }

    private IEnumerator SendRequest()
    {
        WWWForm data = new WWWForm();
        post post1 = new post()
        {
            userId = 123,
            title = "Header",
            body = "PostRequest"

        };
        string json = JsonUtility.ToJson(post1);
        
        UnityWebRequest request = UnityWebRequest.Post(url,data);

        byte[] postBytes = Encoding.UTF8.GetBytes(json);

        UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);

        request.uploadHandler = uploadHandler;
        
        request.SetRequestHeader("Content-type", "application/json; charset=UTF-8");

        yield return request.SendWebRequest();

        post postfromserver = JsonUtility.FromJson<post>(request.downloadHandler.text);
        
        Debug.Log("UserId : " + postfromserver.userId);
        Debug.Log("body : " + postfromserver.body);
        Debug.Log("title : " + postfromserver.title);
        
    }
}
