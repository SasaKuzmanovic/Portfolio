using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;

[System.Serializable]
public class GameData
{
    public int game_id;
    public float Time_Required;
    public int Level;
    public string sessionID;
}


public class Mailman : MonoBehaviour
{
    [System.Obsolete]
    public static IEnumerator PostMethod(string jsonData)
    {
        string url = "https://atomic-diner.anvil.app/_/api/atomic";

        using(UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
                Debug.Log("Data successfully sent to the server");
            else
                Debug.Log("Error sending data to the server: Error " + request.responseCode);
        }
    }
}


