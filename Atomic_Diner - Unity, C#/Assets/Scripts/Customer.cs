using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public GameObject timer;
    public int timeLimit;
    private bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(runTimer());
    }

    public bool getActive() { return isActive; }

    IEnumerator runTimer()
    {
        Vector3 scale = timer.transform.localScale;
        float scaleRatio = scale.x / timeLimit;
        
        for (int i = 0; i < timeLimit; i++)
        {
            scale.x = scaleRatio * (timeLimit - i);
            timer.transform.localScale = scale;
            yield return new WaitForSeconds(1.0f);
        }
        isActive = false;
    }
}
