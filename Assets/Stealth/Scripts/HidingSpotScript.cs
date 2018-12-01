using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotScript : MonoBehaviour {

    public bool containsSanta;

    private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.enableEmission = false;
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
    }

    public void OnSantaExits()
    {
        containsSanta = false;
        ps.enableEmission = false;
    }
}
