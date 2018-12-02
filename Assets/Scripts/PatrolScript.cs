using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour {

    
    public float speed;
    public float timeBetweenStairs;

    private bool goingRight;
    private float timeSinceLastStairs = 0;
    private bool seesPlayer = false;
    private bool visionBlocked = false;

    Rigidbody2D rb;
    SpriteRenderer sr;
    SantaController santa;
    AudioSource audioS;

    //RAYCAST
    ContactFilter2D contact;
    RaycastHit2D[] hits;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioS = GetComponent<AudioSource>();
        contact = new ContactFilter2D();
        contact.SetLayerMask(LayerMask.GetMask("Player", "Ground"));
        santa = GameObject.FindGameObjectWithTag("Player").GetComponent<SantaController>();

        goingRight = Random.value > 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLastStairs += Time.deltaTime;

        sr.flipX = !goingRight;

        rb.velocity = new Vector2(goingRight ? speed : -speed, 0);

        Bounds bounds = GetComponent<CapsuleCollider2D>().bounds;

        //seesPlayer = false;

        



        if (!santa.hidden)
        {
            Vector2 headPosition = new Vector2(bounds.center.x, bounds.max.y);

            Vector2 dir = new Vector2(santa.transform.position.x - headPosition.x, santa.transform.position.y - headPosition.y);
            // RaycastHit2D hitPlayer = Physics2D.Raycast(headPosition, santa.transform.position, 5f, LayerMask.GetMask("Player"));

            
            RaycastHit2D[] hitPlayer = Physics2D.RaycastAll(headPosition, dir, 5f,LayerMask.GetMask("Player","Ground","Door"));
            //Physics2D.Raycast(headPosition, dir,contact, hits, 5f);
            //Physics2D.RaycastAll((headPosition, dir, contact, hits, 5f);
            if (hitPlayer.Length > 0)
            {
                Debug.Log("HITS : " + hitPlayer.Length);
                Debug.Log("TAGS :" + hitPlayer[0].collider.gameObject.tag);

                if (!hitPlayer[0].collider.gameObject.CompareTag("Player"))
                {
                    visionBlocked = true;
                }
                else
                {
                    visionBlocked = false;
                }


                if (!visionBlocked && ((rb.velocity.x > 0.0f && dir.x > 0.0f) || (rb.velocity.x < 0.0f && dir.x < 0.0f)))
                {
                    
                    if (!audioS.isPlaying && !seesPlayer)
                    {
                        audioS.Play();
                    }
                    seesPlayer = true;

                    Debug.Log("I SEE YOU");
                } else
                {
                    seesPlayer = false;
                }
            }   
        } else
        {
            seesPlayer = false;
        }

        if (seesPlayer)
        {
            goingRight = transform.position.x < santa.gameObject.transform.position.x;
        }
        else
        {
            Debug.Log("WHERE HE IS");
            Vector2 origin = new Vector2(bounds.center.x, bounds.min.y);
            RaycastHit2D hit = Physics2D.Raycast(origin, rb.velocity, 0.5f, LayerMask.GetMask("Ground"));

            if (hit.collider != null)
            {
                goingRight = !goingRight;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!seesPlayer && timeSinceLastStairs > timeBetweenStairs && Random.value > 0.1f && col.gameObject.CompareTag("Elevator"))
        {
            transform.position = col.gameObject.GetComponent<StairsScript>().target.transform.position;
            timeSinceLastStairs = 0;
        }
    }
}
