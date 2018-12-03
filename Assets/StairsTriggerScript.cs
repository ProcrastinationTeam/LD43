using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTriggerScript : MonoBehaviour {

    public List<PatrolScript> npcsInTrigger = new List<PatrolScript>();
    public StairsScript parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            /*// S'il va vers l'entrée
            float xVelocity = col.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            float xPosition = col.gameObject.transform.position.x;
            if ((xVelocity > 0 && xPosition < parent.transform.position.x)
                || (xVelocity < 0 && xPosition > parent.transform.position.x))
            {*/
                if(!npcsInTrigger.Contains(col.gameObject.GetComponent<PatrolScript>())) {
                    npcsInTrigger.Add(col.gameObject.GetComponent<PatrolScript>());
                }
            //}
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        // S'il va vers l'entrée
        float xVelocity = col.gameObject.GetComponent<Rigidbody2D>().velocity.x;
        float xPosition = col.gameObject.transform.position.x;
        if ((xVelocity > 0 && xPosition > parent.transform.position.x + 1)
            || (xVelocity < 0 && xPosition < parent.transform.position.x - 1))
            {
            if (npcsInTrigger.Contains(col.gameObject.GetComponent<PatrolScript>()))
            {
                npcsInTrigger.Remove(col.gameObject.GetComponent<PatrolScript>());
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            npcsInTrigger.Remove(col.gameObject.GetComponent<PatrolScript>());
        }
    }
}
