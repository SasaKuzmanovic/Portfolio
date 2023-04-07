using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionComponent 
{

    public static bool AABBCollision(GameObject t_1,GameObject t_2, Collider2D t_1Col,Collider2D t_2Collider)
    {
        if (t_1.transform.position.x < t_2.transform.position.x + t_2Collider.bounds.size.x &&
             // checks to see if object 1 + width is greater than the object 2. postition . x
             t_1.transform.position.x + t_1Col.bounds.size.x > t_2.transform.position.x &&
             // checks to see if object 1 .y is less than object 2 .position.y + height of object 2
             t_1.transform.position.y < t_2.transform.position.y + t_2Collider.bounds.size.y &&
             // checks to see if object 1 position.y is greater than object 2. position + object 2 height
             t_1.transform.position.y + t_1Col.bounds.size.y > t_2.transform.position.y)
        {
            return true;
        }
        return false;
    }



    public static int cube(int x)
    {
        return x * x * x;
    }

    public static int[] cubes(int[] xs)
    {
        int[] result = new int[xs.Length];
        for (int i = 0; i < xs.Length; i++)
        {
            result[i] = cube(xs[i]);
        }
        return result;
    }

}
