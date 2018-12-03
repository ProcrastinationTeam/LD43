using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    SpriteRenderer sr;
    bool isOpen = false;
    public Sprite doorOpen;
    public Sprite doorClose;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!isOpen)
        {
            OpenDoor();
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {

        CloseDoor();

    }

    void CloseDoor()
    {
        isOpen = false;
        sr.sprite = doorClose;
    }

        void OpenDoor()
    {
        isOpen = true;
        sr.sprite = doorOpen;

    }
}
