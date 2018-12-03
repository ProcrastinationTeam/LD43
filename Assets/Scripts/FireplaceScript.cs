using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceScript : MonoBehaviour {

    Animator slurpAnim;

	// Use this for initialization
	void Start () {
        slurpAnim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().SetFireplace(this);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().UnsetFireplace(this);
        }
        
    }

    public void PlayAnim()
    {
        slurpAnim.SetTrigger("Use");
           
    }
}
