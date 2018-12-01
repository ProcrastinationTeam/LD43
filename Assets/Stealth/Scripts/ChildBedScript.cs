﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBedScript : MonoBehaviour {

    public Sprite emptyBedSprite;
    public bool empty = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!empty)
        {
            col.gameObject.GetComponent<SantaController>().SetChildBed(this);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponent<SantaController>().UnsetChildBed(this);
    }

    public void OnSantaKidnaps()
    {
        GetComponent<SpriteRenderer>().sprite = emptyBedSprite;
        empty = true;
    }
}
