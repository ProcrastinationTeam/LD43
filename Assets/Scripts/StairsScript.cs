using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsScript : MonoBehaviour {

    public StairsScript target;
    public List<StairsScript> origins = new List<StairsScript>();
    public StairsTriggerScript stairsTriggerScript;

    bool targetIsSafe = true;

    public Sprite outlined;
    Sprite previous;
    SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Elevator");
        foreach(GameObject go in gos)
        {
            StairsScript tempStairsScript = go.GetComponent<StairsScript>();
            if(tempStairsScript.target == this)
            {
                origins.Add(tempStairsScript);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(targetIsSafe)
        {
            if (target.stairsTriggerScript.npcsInTrigger.Count > 0)
            {
                targetIsSafe = false;
                Debug.Log(gameObject.name + " : NY VAS PAS");
                // TODO: Lancer l'anim clignotante
            }
        } else
        {
            if (target.stairsTriggerScript.npcsInTrigger.Count == 0)
            {
                targetIsSafe = true;
                Debug.Log(gameObject.name + " : gogo");
                // TODO: Lancer l'idle
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().SetStairs(this);
            previous = sr.sprite;
            sr.sprite = outlined;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().UnsetStairs(this);
            sr.sprite = previous;
        }
    }
}