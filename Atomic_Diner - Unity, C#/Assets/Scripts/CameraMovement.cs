using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform camPos1;
    public Transform camPos2;

    //public GameObject image;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    public bool isCameraMoving = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(moveCameraOnScene(camPos2));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(moveCameraOnScene(camPos1));
        }


        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;


            if (endTouchPosition.x < startTouchPosition.x && 
                (startTouchPosition.x - endTouchPosition.x) > 250.0f && 
                startTouchPosition.y < 600.0f)
            {
                moveCamera(camPos1);
                //StartCoroutine(moveCameraOnScene(camPos1));
            }
            if (endTouchPosition.x > startTouchPosition.x && 
                (endTouchPosition.x - startTouchPosition.x) > 250.0f &&
                startTouchPosition.y < 600.0f)
            {
                moveCamera(camPos2);
                //StartCoroutine(moveCameraOnScene(camPos2));
            }
        }


    }

    void moveCamera(Transform t_pos)
    {
        float time = 0;

        while (transform.position.x != t_pos.transform.position.x)
        {
            transform.position = Vector3.Lerp(transform.position, t_pos.transform.position, time * 0.5f);
            time += Time.deltaTime;
        }
        transform.position = t_pos.transform.position;
    }


    IEnumerator moveCameraOnScene(Transform t_pos)
    {
        float time = 0;

        while(transform.position.x != t_pos.transform.position.x)
        {
            transform.position = Vector3.Lerp(transform.position, t_pos.transform.position, time * 0.5f);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = t_pos.transform.position;  
    }
}
