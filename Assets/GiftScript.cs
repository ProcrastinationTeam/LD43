using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.Rotate(Vector3.forward, -3);

        if (this.gameObject.activeInHierarchy)
        {
            CheckIfInScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Fireplace"))
        {
            this.gameObject.SetActive(false);
            collider.gameObject.SetActive(false);
            Debug.Log("IN THE HOLE");
            //ADD SCORE
        }

        if (collider.CompareTag("Enemy"))
        {
            this.gameObject.SetActive(false);
            Debug.Log("GET REKT");
        }
    }


    private void CheckIfInScreen()
    {
        var point = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, Camera.main.nearClipPlane));
        if ((point.x > Camera.main.pixelRect.xMax || point.x < Camera.main.pixelRect.xMin || point.y > Camera.main.pixelRect.yMax || point.y < Camera.main.pixelRect.yMin))
        {
            this.gameObject.SetActive(false);
        }
    }

}
