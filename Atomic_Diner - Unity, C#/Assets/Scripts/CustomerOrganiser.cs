using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CustomerOrganiser : MonoBehaviour
{
    public GameObject[] customerElements;
    private int noCustomerTypes;
    private int customerCounter = 0;
    private List<GameObject> customers;
    public GameObject customerStart;
    public int maxCustomers;
    private bool[] isSpotFree;
    public Transform[] idlePos;

    // Start is called before the first frame update
    void Start()
    {
        customers = new List<GameObject>();
        noCustomerTypes = customerElements.Length;
        StartCoroutine(spawnCustomers());
    }

    private void FixedUpdate()
    {
        if (customers.Count != 0)
        {
            bool deleted = false;
            int counter = 0;
            foreach (var activeCustomer in customers.ToList())
            {
                if(deleted)
                {
                    activeCustomer.GetComponent<CharacterAnimator>().idlePos = idlePos[counter];
                    counter++;
                }
                if(activeCustomer.GetComponent<Customer>().getActive() == false)
                {
                    //GameObject toDestroy = activeCustomer;
                    activeCustomer.GetComponent<CharacterAnimator>().leaving = true;
                    customers.Remove(activeCustomer);
                    //Destroy(toDestroy);
                    deleted = true;
                }
            }
        }
    }

    IEnumerator spawnCustomers()
    {
        while (true)
        { 
            if (customers.Count < maxCustomers)
            {
                GameObject temp = Instantiate(customerElements[customerCounter], customerStart.transform.position, customerStart.transform.rotation);
                temp.GetComponent<CharacterAnimator>().idlePos = idlePos[customers.Count];
                customers.Add(temp);
                customerCounter++;
                if(customerCounter >= noCustomerTypes)
                {
                    customerCounter = 0;
                }
            }
            yield return new WaitForSeconds(10.0f);
        }
    }

    public GameObject getCurrentCustomer()
    {
        GameObject currentCustomer = customers.ElementAt(0).gameObject;
        return currentCustomer;
    }
}
