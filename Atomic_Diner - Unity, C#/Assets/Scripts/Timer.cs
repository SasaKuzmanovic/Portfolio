using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float timer;
    public int currentSceneIndex;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        timer += Time.deltaTime;

        if (currentSceneIndex != scene.buildIndex)
        {
            DataScript.instance.sendData(timer, scene.buildIndex);
            timer = 0;
            Debug.Log("I zink I send sheet");
        }
        
        currentSceneIndex = scene.buildIndex;
    }


    public float sendTimer()
    {
        return timer;
    }

}
