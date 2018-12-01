using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SantaController : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public float afterJumpGravity;


    private List<HidingSpotScript> hidingSpots = new List<HidingSpotScript>();

    private bool hidden = false;
    private HidingSpotScript hidingSpot;

    private bool canJump = true;
    private float baseGravity;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        baseGravity = rb.gravityScale;
    }
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        bool hide = Input.GetButtonDown("Fire1");

        if(Math.Abs(moveHorizontal) >= 0.15 && !hidden)
        {
            rb.velocity = new Vector2(moveHorizontal > 0 ? speed : -speed,  rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(0,                                    rb.velocity.y);
        }
        
        if(canJump && jump && !hidden)
        {
            rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
            canJump = false;
            rb.gravityScale = afterJumpGravity;
        }

        if(hide)
        {
            // HIDE
            if (hidingSpots.Count != 0 && !hidden)
            {
                Hide();
            }
            // UNHIDE
            else if (hidden)
            {
                Unhide();
            }
        }
    }

    public void TouchedGround()
    {
        canJump = true;
        rb.gravityScale = baseGravity;
    }

    public void SetHidingSpot(HidingSpotScript hidingSpot)
    {
        hidingSpots.Add(hidingSpot);
    }

    public void UnsetHidingSpot(HidingSpotScript hidingSpot)
    {
        hidingSpots.Remove(hidingSpot);
    }

    void Hide()
    {
        hidingSpot = hidingSpots[0];
        hidingSpot.OnSantaEnters();
        hidden = true;
        sr.enabled = false;
    }

    void Unhide()
    {
        hidingSpot.OnSantaExits();
        hidingSpot = null;
        hidden = false;
        sr.enabled = true;
    }
}