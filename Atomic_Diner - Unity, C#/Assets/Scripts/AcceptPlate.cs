using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcceptPlate : MonoBehaviour
{
    public PlateController plate;
    public GameObject goodReactionBubble;
    public GameObject badReactionBubble;
    public string reaction = "";

    public bool data = false;
    //public GameObject Plate;
    // Start is called before the first frame update
    void Start()
    {
        data = false;   
    }

    // Update is called once per frame
    void Update()
    {
        plate = GameObject.FindGameObjectWithTag("Plate").GetComponent<PlateController>();
    }

    void chooseState()
    {
        if (plate.returnIngredients().Count == 0)
        {
            reaction = "good";
            TriggerReaction();
        }
        if (plate.returnIngredients().Count > 0)
        {
            reaction = "bad";
            TriggerReaction();
        }
    }

    IEnumerator showGoodReaction()
    {
        goodReactionBubble.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        goodReactionBubble.SetActive(false);
    }

    IEnumerator showBadReaction()
    {
        badReactionBubble.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        badReactionBubble.SetActive(false);
    }

    void TriggerReaction()
    {
        if (reaction == "good")
        {
            StartCoroutine(showGoodReaction());
            data = true;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
            Debug.Log("Good");
        }
        if (reaction == "bad")
        {
            StartCoroutine(showBadReaction());
            data = true;
        }
        //plate.resetPlate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plate"))
        {
            chooseState();
            TriggerReaction();            
        }
    }
}
