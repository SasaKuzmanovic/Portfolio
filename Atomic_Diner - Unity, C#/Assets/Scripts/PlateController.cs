using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    public GameObject meatballs;
    public GameObject pasta;
    public GameObject sauce;
    public GameObject cheese;
    public GameObject mushroom;
    public GameObject spider;
    public GameObject bacon;
    public GameObject mint;
    public GameObject octopus;
    public GameObject pepper;
    public GameObject salt;
    public GameObject bone;
    public GameObject fishBone;
    public GameObject worm;

    bool isMoving = false;

    GameObject food = null;
    public CustomerOrganiser customerOrganiser;

    public List<GameObject> requiredIngredients;

    public Transform plateStartingPos;

    bool customerServed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (customerServed)
        {
            resetPlate();
            customerServed = false;
        }

        if (customerOrganiser.getCurrentCustomer().gameObject.GetComponent<CharacterAnimator>().chosenFood && food == null)
		{
            food = GameObject.FindGameObjectWithTag("RequestedFood");
            getIngredientList();
        }
        //Debug.Log(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set the z position to 0 so that it appears 2D
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);
            if (collider == GetComponent<Collider2D>())
            {
                // Mouse was on the game object, so start moving the game object with the mouse
                isMoving = true;
            }
        } 

        if (isMoving)
        {
            // Move the game object with the mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set the z position to 0 so that it appears 2D
            transform.position = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
            transform.position = plateStartingPos.position;
        }
        
        if(food != null)
        {
            checkFood();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isMoving)
        {
            Vector3 position = transform.position;
            float zRotation = Random.Range(0, 360); // Random z-axis rotation
            Quaternion rotation = Quaternion.Euler(0, 0, zRotation);

            GameObject food = new GameObject();

            string colliderTag = collision.gameObject.tag;

            switch(colliderTag)
            {
                case "Meatballs":
                    food = meatballs;
                    break;
                case "Pasta":
                    food = pasta;
                    break;
                case "Sauce":
                    food = sauce;
                    break;
                case "Cheese":
                    food = cheese;
                    break;
                case "Mushroom":
                    food = mushroom;
                    break;
                case "Spider":
                    food = spider;
                    break;
                case "Bacon":
                    food = bacon;
                    break;
                case "Mint":
                    food = mint;
                    break;
                case "Octopus":
                    food = octopus;
                    break;
                case "Pepper":
                    food = pepper;
                    break;
                case "Salt":
                    food = salt;
                    break;
                case "Bone":
                    food = bone;
                    break;
                case "FishBone":
                    food = fishBone;
                    break;
                case "Worm":
                    food = worm;
                    break;
            }

            Instantiate(food, position, rotation);
            food.transform.position = position;
        }
        if(collision.gameObject.CompareTag("Customer"))
        {
            customerServed = true;
        }
    }

    public void getIngredientList()
    {
        requiredIngredients = food.GetComponent<RequestedFood>().ingredientList;
        foreach (GameObject ingredients in requiredIngredients)
        {
            Debug.Log(ingredients.gameObject.tag);
        }
    }

    public List<GameObject> returnIngredients()
    {
        return requiredIngredients;
    }

    void checkFood()
    {
        int childrenNum = transform.childCount;

        if(requiredIngredients.Count > 0)
        {
            for (int i = 0; i < childrenNum; i++)
            {
                foreach (GameObject ingredient in requiredIngredients)
                {
                    if (ingredient.gameObject.tag == transform.GetChild(i).gameObject.tag)
                    {
                        requiredIngredients.Remove(ingredient);
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("all ingredients on plate");
        }
    }

    public void resetPlate()
    {
        int childrenNum = transform.childCount;

        if (childrenNum > 0)
        {
            for (int i = 0; i < childrenNum; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }     
    }
}
