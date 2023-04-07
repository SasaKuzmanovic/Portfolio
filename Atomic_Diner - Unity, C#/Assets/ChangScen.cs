using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangScen : MonoBehaviour
{
    public static ChangScen instance;

    public void ChangeScene()
    {
        //SceneManager.LoadScene("Level2");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
