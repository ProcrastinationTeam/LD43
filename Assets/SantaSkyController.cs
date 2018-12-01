using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaSkyController : MonoBehaviour {

    int dirX = 0;
    int dirY = 0;

    public Rigidbody2D playerRigid;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.15f)
        {
            dirX = (Input.GetAxis("Horizontal") > 0.0f ? 1 : -1);
        }
        else
        {
            dirX = 0;
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) >= 0.15f)
        {
             dirY = (Input.GetAxis("Vertical") > 0.0f ? 1 : -1);
            // dirY = Input.GetAxis("Vertical");
        }
        else
        {
            dirY = 0;
        }

        playerRigid.velocity = new Vector2(dirX, dirY);


    }
}
