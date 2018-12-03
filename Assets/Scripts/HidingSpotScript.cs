using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotScript : MonoBehaviour {

    public bool containsSanta;
    private Sprite originalSprite;
    public Sprite alternativeSprite;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        originalSprite = sr.sprite;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().SetHidingSpot(this);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().UnsetHidingSpot(this);
        }
    }

    public void OnSantaEnters()
    {
        containsSanta = true;
        sr.sprite = alternativeSprite;

    }

    public void OnSantaExits()
    {
        containsSanta = false;
        sr.sprite = originalSprite;
    }
}
