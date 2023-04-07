using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    private bool isMoving = false; // Whether the game object is currently being moved

    Vector2 originalPos;

    bool collideWithPlate = false;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was on the game object when the mouse button was clicked
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set the z position to 0 so that it appears 2D
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);
            if (collider == GetComponent<Collider2D>())
            {
                // Mouse was on the game object, so start moving the game object with the mouse
                isMoving = true;
                collideWithPlate = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Stop moving the game object when the mouse button is released
            isMoving = false;
        }

        if (isMoving)
        {
            // Move the game object with the mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set the z position to 0 so that it appears 2D
            transform.position = mousePosition;
        }

        if(collideWithPlate == true)
        {
            transform.position = originalPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plate"))
        {
            collideWithPlate = true;
        }
    }
}
