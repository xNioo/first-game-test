using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayerController : MonoBehaviour
{
    [SerializeField] public LayerMask platformLayerMask;

    public Animator animator;
    public float jumppower;
    public int maxjumps;
    public float speed;
    public float dashpower;
    public int dashnumber;
    public BoxCollider2D bd;
    public Rigidbody2D rb;
    private float walk;
    private float falling;
    private int dashes = 0;
    private int jumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        walk = Input.GetAxis("Horizontal");

        Vector3 characterScale = transform.localScale;

        animator.SetFloat("Speed", Mathf.Abs(walk));

        // Stop Dash after landing
        if (GroundCheck() && (!animator.GetBool("IsJumping") && !animator.GetBool("IsFalling")))
        {
            rb.linearVelocity = Vector2.zero;
            falling = 0;
            dashes = 1;
            jumps = 1;
        }

        // Links Drehung
        if (walk < 0)
        {
            characterScale.x = -3;
        }

        // Rechts Drehung
        if (walk > 0)
        {
            characterScale.x = 3;
        }
        transform.localScale = characterScale;

        // Walking
        if ((walk < 0 && !WalkCheckLeft()) || (walk > 0 && !WalkCheckRight()))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed * walk);
        }

        // Dash
        if (Input.GetButtonDown("Dash") && dashes <= dashnumber)
        {
            rb.AddForce(Vector2.right * walk * dashpower, ForceMode2D.Impulse);
            dashes++;
        }

        // Jump
        if (Input.GetButtonDown("Jump") && jumps <= maxjumps)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsJumping", true);
            jumps++;
        }

        falling = rb.linearVelocity.y;

        // Falling
        if (falling < 0)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }
        
    }   

    // Checkt ob ich Springen kann
    private bool GroundCheck()
    {
        return Physics2D.BoxCast(bd.bounds.center, bd.bounds.size, 0f, Vector2.down, .1f, platformLayerMask);
    }
    
    //Schaut ob Rechts vom Spieler Hindernisse sind
    private bool WalkCheckRight()
    {
        return Physics2D.BoxCast(bd.bounds.center, bd.bounds.size, 0f, Vector2.right, .01f, platformLayerMask);
    }

    //Schaut ob Links vom Spieler Hindernisse sind
    private bool WalkCheckLeft()
    {
        return Physics2D.BoxCast(bd.bounds.center, bd.bounds.size, 0f, Vector2.left, .01f, platformLayerMask);
    }
}
    
    

