using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DataScript : MonoBehaviour
{
    public int game = 0;

    public bool not = true;
    public bool gameEnded = false;

    private string sessionIDDevice;
    public static DataScript instance = null;

    public AcceptPlate dataPlate;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       
        game = Random.Range(0, 80000);
        Debug.Log("Game ID: " + game);

        sessionIDDevice = SystemInfo.deviceUniqueIdentifier;
    }


    [System.Obsolete]
    public void sendData(float t_timer, int t_currentLevel)
    {
        sessionIDDevice = sessionIDDevice = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-") + sessionIDDevice;

        GameData data = new GameData
        {
            game_id = game,
            Time_Required = t_timer,
            Level = t_currentLevel,
            sessionID = sessionIDDevice
        };
        string jsonData = JsonUtility.ToJson(data);
        StartCoroutine(Mailman.PostMethod(jsonData));
    }
}
