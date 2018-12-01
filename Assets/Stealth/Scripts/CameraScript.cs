using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public SantaController santaController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(santaController.canJump)
        {
            transform.position = new Vector3(santaController.transform.position.x, santaController.transform.position.y + 1.25f, -10);
        } else
        {
            transform.position = new Vector3(santaController.transform.position.x, transform.position.y, -10);
        }
        
    }
}
