using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsScript : MonoBehaviour {

    public StairsScript otherStairsScript;
    public SantaController santaController;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<SantaController>().SetStairs(this);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponent<SantaController>().UnsetStairs(this);
    }
}