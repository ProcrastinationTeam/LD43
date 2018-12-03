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
    Animator anim;

    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Elevator");
        foreach(GameObject go in gos)
        {
            StairsScript tempStairsScript = go.GetComponent<StairsScript>();
            if(tempStairsScript.target == this)
            {
                origins.Add(tempStairsScript);
            }
        }

        audioSource = GetComponent<AudioSource>();
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
                anim.SetBool("EnemyNear", true);
            }
        } else
        {
            if (target.stairsTriggerScript.npcsInTrigger.Count == 0)
            {
                targetIsSafe = true;
                Debug.Log(gameObject.name + " : gogo");
                // TODO: Lancer l'idle
                anim.SetBool("EnemyNear", false);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().SetStairs(this);
            anim.SetBool("PlayerNear", true);
          
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            anim.SetBool("PlayerNear", false);
            col.gameObject.GetComponent<SantaController>().UnsetStairs(this);
            
        }
    }

    public void Use()
    {
        audioSource.Play();
    }
}