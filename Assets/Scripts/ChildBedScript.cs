using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBedScript : MonoBehaviour {

    public Sprite emptyBedSprite;
    public bool empty = false;

    public Sprite outlined;
    Sprite previous;
    SpriteRenderer sr;


    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!empty && col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().SetChildBed(this);
            previous = sr.sprite;
            sr.sprite = outlined;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!empty)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<SantaController>().UnsetChildBed(this);
                sr.sprite = previous;
            }
        }
    }

    public void OnSantaKidnaps()
    {
        GetComponent<SpriteRenderer>().sprite = emptyBedSprite;
        empty = true;
    }
}
