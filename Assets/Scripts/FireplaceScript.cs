using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceScript : MonoBehaviour {

    Animator slurpAnim;

    SpriteRenderer sr;

    AudioSource audioSource;
    public AudioClip merryChristmas;
    public AudioClip byebyechild;

    // Use this for initialization
    void Start () {
        slurpAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.clip = byebyechild;
        audioSource.Play();

        StartCoroutine(FoutageDeGueule());
    }

    IEnumerator FoutageDeGueule()
    {
        yield return new WaitForSeconds(1.7f);
        audioSource.clip = merryChristmas;
        audioSource.Play();
    }
}
