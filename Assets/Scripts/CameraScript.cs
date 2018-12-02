using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private SantaController santaController;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime;

    private bool catchUp = false;

    /*
    public BoxCollider2D cameraBounds;
    private float horzExtent;*/

    // Use this for initialization
    void Start () {
        /*horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        Debug.Log(horzExtent);*/
        santaController = GameObject.FindGameObjectWithTag("Player").GetComponent<SantaController>();
    }

    public void GoCameraGo()
    {
        catchUp = true;
    }

    public void EasyGirl()
    {
        catchUp = false;
    }

    // Update is called once per frame
    void Update () {
        Vector3 targetPosition;
        
        if(catchUp)
        {
            targetPosition = new Vector3(santaController.transform.position.x, santaController.transform.position.y - 3.5f,     -10);
        }
        else if (santaController.canJump)
        {
            targetPosition = new Vector3(santaController.transform.position.x, santaController.transform.position.y + 1.25f,    -10);
        } else
        {
            targetPosition = new Vector3(santaController.transform.position.x, transform.position.y,                            -10);
        }

        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        /*newPosition.x = Mathf.Clamp(newPosition.x, cameraBounds.transform.position.x, cameraBounds.transform.position.x + cameraBounds.size.x);*/

        transform.position = newPosition;
    }
}
