using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadArt : MonoBehaviour
{
    public RawImage RawImage;
    public string Url;

    void Start()
    {
        StartCoroutine(DownloadImage(Url));
    }

    private IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            RawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
