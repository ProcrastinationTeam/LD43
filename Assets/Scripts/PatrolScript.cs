using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour {

    
    public float speed;
    public float timeBetweenStairs;

    private bool goingRight;
    private float timeSinceLastStairs = 0;
    private bool seesPlayer = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    SantaController santa;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        santa = GameObject.FindGameObjectWithTag("Player").GetComponent<SantaController>();

        goingRight = Random.value > 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLastStairs += Time.deltaTime;

        sr.flipX = !goingRight;

        rb.velocity = new Vector2(goingRight ? speed : -speed, 0);

        Bounds bounds = GetComponent<CapsuleCollider2D>().bounds;

        if(seesPlayer)
        {
            goingRight = transform.position.x < santa.gameObject.transform.position.x;
        }
        else
        {
            Vector2 origin = new Vector2(bounds.center.x, bounds.min.y);
            RaycastHit2D hit = Physics2D.Raycast(origin, rb.velocity, 0.5f, LayerMask.GetMask("Ground"));

            if (hit.collider != null)
            {
                goingRight = !goingRight;
            }
        }

        if (!santa.hidden)
        {
            Vector2 headPosition = new Vector2(bounds.center.x, bounds.max.y);
            RaycastHit2D hitPlayer = Physics2D.Raycast(headPosition, santa.transform.position, 3f, LayerMask.GetMask("Player"));

            if (hitPlayer.collider != null)
            {
                seesPlayer = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!seesPlayer && timeSinceLastStairs > timeBetweenStairs && Random.value > 0.1f)
        {
            transform.position = col.gameObject.GetComponent<StairsScript>().target.transform.position;
            timeSinceLastStairs = 0;
        }
    }
}
