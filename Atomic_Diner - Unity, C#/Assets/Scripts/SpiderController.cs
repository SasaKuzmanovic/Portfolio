using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed;
    private float distance;
    private float randomDistance;

    bool moveToA = true;

    // Start is called before the first frame update
    void Start()
    {
        randomDistance = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = moveToA ? pointA.position : pointB.position;
        distance = Vector3.Distance(transform.position, target);
        if (distance > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }
        else
        {
            moveToA = !moveToA;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plate"))
        {
            Destroy(gameObject);
        }
    }
}
