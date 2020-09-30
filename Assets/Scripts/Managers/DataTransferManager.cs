using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataTransferManager : MonoBehaviour
{
    [SerializeField]
    Backpack backpack;

    public void SendData()
    {
        StartCoroutine("SendDataAsync");
    }

    IEnumerator SendDataAsync()
    {
        var formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("auth: \"BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6\""));
        formData.Add(new MultipartFormDataSection($"id: {backpack.ActiveItem.Config.Id}"));
        formData.Add(new MultipartFormDataSection($"operation: {backpack.ActiveOperation}"));

        var www = UnityWebRequest.Post("https://dev3r02.elysium.today/inventory/status", formData);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Uploaded succesfully");
        }
    }
}
