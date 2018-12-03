using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceScript : MonoBehaviour {

    Animator slurpAnim;

    SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        slurpAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().SetFireplace(this);
            slurpAnim.SetBool("PlayerNear", true);
        }        

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<SantaController>().UnsetFireplace(this);

            slurpAnim.SetBool("PlayerNear", false);
        }
    }

    public void PlayAnim()
    {
        slurpAnim.SetTrigger("Use");
    }
}
