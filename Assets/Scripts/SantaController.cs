using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class SantaController : MonoBehaviour
{

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
    float baseGravity;
    float timeSinceJump = 1;
    bool jumped = false;

    // STAIRS
    private List<StairsScript> stairs = new List<StairsScript>();

    // FIREPLACE(S)
    private List<FireplaceScript> fireplaces = new List<FireplaceScript>();

    // CHILDREN BEDS
    private List<ChildBedScript> childrenBedsInReach = new List<ChildBedScript>();

    private bool hasChild = false;
    public int numberOfChildrenKidnaped = 0;
    public int numberOfChildrenBeds;

    // COMPONENTS
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CapsuleCollider2D box2d;

    //ANIMATION
    private Animator animator;

    // UI
    UIScript ui;

    CameraScript camera;

    IEnumerator goCameraGoCoroutine;

    // 
    //public FireplaceScript startingFireplace;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        box2d = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        baseGravity = rb.gravityScale;

        //transform.position = startingFireplace.transform.position;
        numberOfChildrenBeds = GameObject.FindGameObjectsWithTag("ChildBed").Length;

        ui = GameObject.Find("Canvas").GetComponent<UIScript>();

        camera = GameObject.Find("Main Camera").GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        bool action = Input.GetButtonDown("Fire1");

        if(jumped)
        {
            timeSinceJump += Time.deltaTime;
        }

        if (Math.Abs(moveHorizontal) >= 0.15 && !hidden)
        {
            animator.SetFloat("velocity", 0.5f);
            rb.velocity = new Vector2(moveHorizontal > 0 ? speed : -speed, rb.velocity.y);
            if (moveHorizontal > 0.0f)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
        else
        {
            animator.SetFloat("velocity", -1f);
            rb.velocity = new Vector2(0, rb.velocity.y);

        }

        if (action)
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
            else if (stairs.Count != 0)
            {
                UseStairs();
            }
            // FIREPLACE(S)
            else if (fireplaces.Count != 0)
            {
                UseFireplace();
            }
            // CHILDREN BEDS
            else if (childrenBedsInReach.Count != 0)
            {
                UseChildBed();
            }
        }

        if (canJump && jump && !hidden)
        {
            Jumped();
        } else
        {
            Bounds bounds = GetComponent<CapsuleCollider2D>().bounds;
            Vector2 bottomPosition = new Vector2(bounds.center.x, bounds.min.y);
            RaycastHit2D hit = Physics2D.Raycast(bottomPosition, Vector2.down, 0.05f, LayerMask.GetMask("Ground", "OneWay", "FirstOneWay"));

            // Si y'a un sol juste en dessous
            if (hit.collider != null)
            {
                // Si on peut pas sauter (on était donc en train de tomber)
                if (!canJump && rb.velocity.y <= 0 && timeSinceJump > 0.1f)
                {
                    TouchedGround();
                }
                // Si on peut sauter, c'est juste qu'on marche
                else
                {
                    //
                }
            }
            // Y'a rien juste en dessous
            else
            {
                // Si on peut (encore) sauter, c'est qu'on vient de walk off
                if (canJump)
                {
                    Felt();
                } 
                // Si on peut plus, c'est qu'on tombait déjà
                else
                {
                    //
                }
            }
        }

        CheckVisiblityUse();
    }

    void Jumped()
    {
        rb.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
        jumped = true;
        LeftGround();
        goCameraGoCoroutine = GoCameraGo();
        StartCoroutine(goCameraGoCoroutine);
    }

    void Felt()
    {
        LeftGround();
        timeSinceJump = 1;
        camera.GoCameraGo();
    }

    void LeftGround()
    {
        animator.SetBool("isJumping", true);
        canJump = false;
        rb.gravityScale = baseGravity * afterJumpGravityMultiplier;
    }

    IEnumerator GoCameraGo()
    {
        yield return new WaitForSeconds(0.5f);
        if (!canJump)
        {
            camera.GoCameraGo();
        }
    }

    public void TouchedGround()
    {
        animator.SetBool("isJumping", false);
        canJump = true;
        jumped = false;
        timeSinceJump = 0;
        rb.gravityScale = baseGravity;
        camera.EasyGirl();
        if(goCameraGoCoroutine != null)
        {
            StopCoroutine(goCameraGoCoroutine);
        }
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
        
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
    }

    void Unhide()
    {
        hidingSpot.OnSantaExits();
        hidingSpot = null;
        hidden = false;
        sr.enabled = true;
        
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
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
        if (hasChild)
        {
            numberOfChildrenKidnaped++;
            hasChild = false;
            animator.SetBool("hasChild", false);
            fireplaces[0].PlayAnim();
            ui.OnSantaReleased();
            if (numberOfChildrenKidnaped == numberOfChildrenBeds)
            {
                // C'était le dernier gosse
                // Afficher un message indiquant qu'il faut se casser
            }
        }
        else
        {
            if (numberOfChildrenKidnaped == numberOfChildrenBeds)
            {
                GoToNextScene();
            }
            else
            {
                ui.OnSantaTriesToExitWithoutChild();
            }
        }
    }

    void GoToNextScene()
    {
        if (numberOfChildrenBeds == numberOfChildrenKidnaped)
        {
            // TODO: Beaucoup de points
        } else
        {
            // TODO: Moins de points
        }

        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNumber + 1);

        /*
        string sceneName = SceneManager.GetActiveScene().name;
        int sceneNumber = Int32.Parse(Regex.Match(sceneName, @"\d+").Value);
        SceneManager.LoadScene("House_" + (sceneNumber + 1));
        */
    }

    // CHILDREN BEDS
    public void SetChildBed(ChildBedScript childBed)
    {
        childrenBedsInReach.Add(childBed);
    }

    public void UnsetChildBed(ChildBedScript childBed)
    {
        childrenBedsInReach.Remove(childBed);
    }

    void UseChildBed()
    {
        if (!hasChild)
        {
            hasChild = true;
            animator.SetBool("hasChild", true);
            childrenBedsInReach[0].OnSantaKidnaps();
            ui.OnSantaSnatched();
        }
        else
        {
            Debug.Log("TODO: Message d'erreur");
        }
    }

    void CheckVisiblityUse()
    {
        // Si une liste pas empty, afficher
        if(childrenBedsInReach.Count > 0 
            || fireplaces.Count > 0
            || stairs.Count > 0
            || hidingSpots.Count > 0)
        {
            ui.DisplayUseText(true);
        }
        else
        {
            ui.DisplayUseText(false);
        }
    }

}