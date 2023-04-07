using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestedFood : MonoBehaviour
{
    GameObject food;
    public GameObject pasta_cheese_sauce; // done
    public GameObject pasta_sauce_meatball; // done
    public GameObject pasta_sauce_meatball_cheese_mushroom;
    public GameObject pasta_mushroom_meatball;
    public GameObject pasta_mushroom_meatball_sauce;

    public List<GameObject> ingredientList;

    public GameObject pasta;
    public GameObject sauce;
    public GameObject cheese;
    public GameObject meatball;
    public GameObject mushroom;

    // Start is called before the first frame update
    void Start()
    {
        randomFood();
        spawnFood();
        checkIngredients();
        foreach(GameObject ingredients in ingredientList)
        {
           // Debug.Log(ingredients.gameObject.tag);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnFood()
    {
        GameObject gameObject = Instantiate(food, transform.position, transform.rotation,this.transform);
        gameObject.transform.parent = transform;
    }   

    void checkIngredients()
    {
        ingredientList.Add(pasta);
        if (food.CompareTag("Spaghetti_Cheese_Sauce"))
        {
            ingredientList.Add(sauce);
            ingredientList.Add(cheese);
        }

        if (food.CompareTag("Spaghetti_Sauce_Meatball"))
        {
            ingredientList.Add(sauce);
            ingredientList.Add(meatball);
        }

        if (food.CompareTag("Spaghetti_Sauce_Meatball_Cheese_Mushroom"))
        {
            ingredientList.Add(sauce);
            ingredientList.Add(meatball);
            ingredientList.Add(mushroom);
            ingredientList.Add(cheese);
        }

        if (food.CompareTag("Spaghetti_Mushroom_Meatball"))
        {
            ingredientList.Add(meatball);
            ingredientList.Add(mushroom);
        }

        if (food.CompareTag("Spaghetti_Mushroom_Meatball_Sauce"))
        {
            ingredientList.Add(sauce);
            ingredientList.Add(meatball);
            ingredientList.Add(mushroom);
        }
    }

    void randomFood()
    {
        int randomNum = Random.Range(0, 4);

        switch(randomNum)
        {
            case 0:
                food = pasta_cheese_sauce;
                break;
            case 1:
                food = pasta_sauce_meatball;
                break;
            case 2:
                food = pasta_sauce_meatball_cheese_mushroom;
                break;
            case 3:
                food = pasta_mushroom_meatball;
                break;
            case 4:
                food = pasta_mushroom_meatball_sauce;
                break;
        }
    }    
}
