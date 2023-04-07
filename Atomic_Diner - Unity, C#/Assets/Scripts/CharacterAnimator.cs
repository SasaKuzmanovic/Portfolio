using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;
    public bool Moving;

    public Transform idlePos;
    private Vector3 startPos;
    public ParticleSystem dust;

    public bool chosenFood = false;

    public GameObject requestedFood;
    public Transform foodPos;

    public bool leaving = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dust = GetComponentInChildren<ParticleSystem>();
        startPos = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("IdlePosition"))
        {
            //Debug.Log("We have hit the idle position");
            animator.SetBool("Moving", false);
        }
        if (collision.gameObject.CompareTag("Door"))
        {
            if (leaving)
            {
                Destroy(this.gameObject);
            }
        }
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("IdlePosition"))
    //    {
    //        animator.SetBool("goIdle", true);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (!leaving)
        {
            if (gameObject.transform.position != idlePos.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, idlePos.position, 0.05f);
                CreateDustEffect();
            }
        }
        else
        {
            if (gameObject.transform.position != startPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, 0.05f);
                CreateDustEffect();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //stopDustEffect();
            animator.SetBool("Moving", false);
        }

        if (Moving)
        {
            CreateDustEffect();
            animator.SetBool("Moving", true);
        }

        if(gameObject.transform.position == idlePos.transform.position && !chosenFood)
        {
            chosenFood = true;

            GameObject gameObject = Instantiate(requestedFood, foodPos.position, transform.rotation,this.transform);
        }
    }

    void CreateDustEffect()
    {
        //Debug.Log("We are currently playing dust particle effect ");
        dust.Play();
    }

    void stopDustEffect()
    {
        dust.Stop();
    }
}
