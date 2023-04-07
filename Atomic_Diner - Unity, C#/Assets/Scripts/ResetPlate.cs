using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlate : MonoBehaviour
{
    public GameObject plate;
    public Transform plateOriginalPosition;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetPlate()
    {
        int childrenNum = plate.transform.childCount;

        if(childrenNum > 0)
        {
           for(int i = 0; i < childrenNum; i++)
           {
               Destroy(plate.transform.GetChild(i).gameObject); 
           }
        }

        plate.transform.position = plateOriginalPosition.position;
    }
}
