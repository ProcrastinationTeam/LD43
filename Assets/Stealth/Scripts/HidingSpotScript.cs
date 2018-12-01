using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotScript : MonoBehaviour {

    public bool containsSanta;
    private Sprite originalSprite;
    public Sprite alternativeSprite;

    private ParticleSystem ps;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.enableEmission = false;
        sr = GetComponent<SpriteRenderer>();
        originalSprite = sr.sprite;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<SantaController>().SetHidingSpot(this);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponent<SantaController>().UnsetHidingSpot(this);
    }

    public void OnSantaEnters()
    {
        containsSanta = true;
        ps.enableEmission = true;
        sr.sprite = alternativeSprite;
    }

    public void OnSantaExits()
    {
        containsSanta = false;
        ps.enableEmission = false;
        sr.sprite = originalSprite;
    }
}
