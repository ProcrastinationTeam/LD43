﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotScript : MonoBehaviour {

    public bool containsSanta;
    private Sprite originalSprite;
    public Sprite alternativeSprite;

    public Sprite outlined;
    Sprite previous;
    SpriteRenderer sr;

    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        originalSprite = sr.sprite;
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().SetHidingSpot(this);
            previous = sr.sprite;
            sr.sprite = outlined;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().UnsetHidingSpot(this);
            sr.sprite = previous;

        }
    }

    public void OnSantaEnters()
    {
        containsSanta = true;
        sr.sprite = alternativeSprite;
        audioSource.Play();
    }

    public void OnSantaExits()
    {
        containsSanta = false;
        sr.sprite = outlined;
        audioSource.Play();
    }
}
