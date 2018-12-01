using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaSkyController : MonoBehaviour {

    float dirX = 0f;
    float dirY = 0f;

    public Rigidbody2D playerRigid;
    public PlayerManagerScript playerManager;
    public GameObject giftPrefab;
    public int giftSpeed;
    public int playerSpeed;

    //POOL
    List<GameObject> gifts;
    List<Rigidbody2D> giftsRigid;
    public int pooledAmount = 100;



    // Use this for initialization
    void Start () {
        gifts = new List<GameObject>();
        giftsRigid = new List<Rigidbody2D>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(giftPrefab);
            obj.SetActive(false);
            gifts.Add(obj);
            giftsRigid.Add(obj.GetComponent<Rigidbody2D>());
        }


    }
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.15f)
        {
            //dirX = (Input.GetAxis("Horizontal") > 0.0f ? 1 : -1);
            dirX = Input.GetAxis("Horizontal");
        }
        else
        {
            dirX = 0f;
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) >= 0.15f)
        {
             dirY = (Input.GetAxis("Vertical") > 0.0f ? 1 : -1);
            // dirY = Input.GetAxis("Vertical");
        }
        else
        {
            dirY = 0f;
        }

        playerRigid.velocity = new Vector2(dirX, dirY) * playerSpeed;



        if(Input.GetButtonDown("Fire1"))
        {
            Shoot(this.transform.right);
        }


    }

    void Shoot(Vector2 directeur)
    {
        playerManager.Shoot();
        for (int i = 0; i < gifts.Count; i++)
        {
            if (!gifts[i].activeInHierarchy)
            {
                gifts[i].transform.position = this.transform.position;
                gifts[i].SetActive(true);

                var dir = directeur.normalized;
                giftsRigid[i].AddForce(dir * giftSpeed, ForceMode2D.Impulse);

                break;
            }
        }
    }

}
