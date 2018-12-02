﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SantaController : MonoBehaviour {

    // MOVEMENT
    public float speed;
    public float jumpForce;
    public float afterJumpGravityMultiplier;




    // HIDING SPOTS
    private List<HidingSpotScript> hidingSpots = new List<HidingSpotScript>();
    public bool hidden = false;
    private HidingSpotScript hidingSpot;

    // MOVEMENT
    public bool canJump = true;
    private float baseGravity;

    // STAIRS
    private List<StairsScript> stairs = new List<StairsScript>();

    // FIREPLACE(S)
    private List<FireplaceScript> fireplaces = new List<FireplaceScript>();

    // CHILDREN BEDS
    private List<ChildBedScript> childrenBeds = new List<ChildBedScript>();

    private int childrenCount = 0;

    // COMPONENTS
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    //ANIMATION
    private Animator animator;

    // 
    //public FireplaceScript startingFireplace;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        baseGravity = rb.gravityScale;

        //transform.position = startingFireplace.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        bool action = Input.GetButtonDown("Fire1");

        if(Math.Abs(moveHorizontal) >= 0.15 && !hidden)
        {
            animator.SetFloat("velocity", 0.5f);
            rb.velocity = new Vector2(moveHorizontal > 0 ? speed : -speed,  rb.velocity.y);
            if(moveHorizontal > 0.0f)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        } else
        {
            animator.SetFloat("velocity", -1f);
            rb.velocity = new Vector2(0,                                    rb.velocity.y);

        }
        
        if(canJump && jump && !hidden)
        {
            animator.SetBool("isJumping", true);
            rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
            canJump = false;
            rb.gravityScale = baseGravity * afterJumpGravityMultiplier;
        }

        if(action)
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
            // STAIRS
            else if(stairs.Count != 0) {
                UseStairs();
            }
            // FIREPLACE(S)
            else if (fireplaces.Count != 0)
            {
                UseFireplace();
            }
            // CHILDREN BEDS
            else if (childrenBeds.Count != 0)
            {
                UseChildBed();
            }
        }

        Bounds bounds = GetComponent<CapsuleCollider2D>().bounds;
        Vector2 bottomPosition = new Vector2(bounds.center.x, bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(bottomPosition, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            TouchedGround();
        } else
        {
            canJump = false;
            rb.gravityScale = baseGravity * afterJumpGravityMultiplier;
        }
    }

    public void TouchedGround()
    {
        animator.SetBool("isJumping", false);
        canJump = true;
        rb.gravityScale = baseGravity;
    }

    // HIDING SPOT
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

    // STAIRS
    public void SetStairs(StairsScript stairsScript)
    {
        stairs.Add(stairsScript);
    }

    public void UnsetStairs(StairsScript stairsScript)
    {
        stairs.Remove(stairsScript);
    }

    void UseStairs()
    {
        transform.position = stairs[0].target.transform.position;
    }

    // FIREPLACE(S)
    public void SetFireplace(FireplaceScript fireplace)
    {
        fireplaces.Add(fireplace);
    }

    public void UnsetFireplace(FireplaceScript fireplace)
    {
        fireplaces.Remove(fireplace);
    }

    void UseFireplace()
    {
        Debug.Log("CASSE TOI");
    }

    // CHILDREN BEDS
    public void SetChildBed(ChildBedScript childBed)
    {
        childrenBeds.Add(childBed);
    }

    public void UnsetChildBed(ChildBedScript childBed)
    {
        childrenBeds.Remove(childBed);
    }

    void UseChildBed()
    {
        childrenBeds[0].OnSantaKidnaps();
        childrenCount++;
        Debug.Log("COUCOU, TU VEUX VOIR MON GIFT ?");
    }
    
    
}