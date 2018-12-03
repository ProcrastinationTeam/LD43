using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatrolScript : MonoBehaviour {

    
    public float speed;
    public float timeBetweenStairs;
    public float jumpForce;

    private bool goingRight;
    private float timeSinceLastStairs = 0;
    private bool seesPlayer = false;
    private bool visionBlocked = false;

    private float timeSinceLastStep = 0;

    public float stepChance;

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
        timeSinceLastStep += Time.deltaTime;

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

        Vector2 justBelowWaist = new Vector2(goingRight ? bounds.max.x : bounds.min.x, bounds.center.y - 0.25f);
        Vector2 justBelowHead = new Vector2(goingRight ? bounds.max.x : bounds.min.x, bounds.max.y - 0.25f);
        RaycastHit2D hitBelowWaist = Physics2D.Raycast(justBelowWaist, rb.velocity, 0.25f, LayerMask.GetMask("Ground", "OneWay"));
        RaycastHit2D hitBelowWaistFirstStep = Physics2D.Raycast(justBelowWaist, rb.velocity, 0.25f, LayerMask.GetMask("FirstOneWay"));
        RaycastHit2D hitBelowHead = Physics2D.Raycast(justBelowHead, rb.velocity, 0.25f, LayerMask.GetMask("Ground"));

        if (seesPlayer)
        {
            goingRight = transform.position.x < santa.gameObject.transform.position.x;
        }
        else
        {
            // Si y'a les 2, demi tour
            if ((hitBelowWaist.collider != null || hitBelowWaistFirstStep.collider != null) && hitBelowHead.collider != null)
            {
                goingRight = !goingRight;
            }
        }
        if ((hitBelowWaist.collider != null && hitBelowHead.collider == null)
            /*|| (hitBelowWaistFirstStep.collider != null && hitBelowHead.collider == null && Random.value > 0.95f)*/)
        {
            rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!seesPlayer && timeSinceLastStairs > timeBetweenStairs && Random.value > 0.1f && col.CompareTag("Elevator"))
        {
            transform.position = col.gameObject.GetComponent<StairsScript>().target.transform.position;
            timeSinceLastStairs = 0;
        }

        if (seesPlayer && col.CompareTag("Player"))
        {
            SceneManager.LoadScene("RetryScene");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("FirstOneWay") && Random.value > stepChance && timeSinceLastStep > 1.0f)
        {
            rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
            timeSinceLastStep = 0;
        }
    }
}
