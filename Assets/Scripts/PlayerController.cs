using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public LayerMask platformLayerMask;

    public float speed;
    public float jumppower;
    public Rigidbody2D rb;
    private BoxCollider2D bc;
    private float walking;
    


    // Start is called before the first frame update
    void Awake()
    {
        bc = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        walking = Input.GetAxis("Horizontal");
        Vector3 characterScale = transform.localScale;


        //Links Drehung
        if (walking < 0)
        {
           characterScale.x = -3;
        }

        //Rechts Drehung
        if(walking > 0)
        {
            characterScale.x = 3;
        }
        transform.localScale = characterScale;

        //Laufen
        if ((walking > 0 && CantWalkRight() == false) || (walking < 0 && CantWalkLeft() == false))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed * walking);
        }

        //Springen
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
        }
    }

    //Schaut ob ich auf dem Boden stehe um springen zu können
    private bool IsGrounded()
    {
        float extraHight = .1f;

        return Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y + extraHight, platformLayerMask);

    }

    //Schaut ob Rechts Blockaden sind die am Laufen hindern
    private bool CantWalkRight()
    {
        float extraWidth = .05f;

        RaycastHit2D raycastHitWalk = Physics2D.Raycast(bc.bounds.center, Vector2.right, bc.bounds.extents.x + extraWidth, platformLayerMask);

        return raycastHitWalk.collider != null;
    }

    //Schaut ob Links Blockaden sind die am Laufen hindern
    private bool CantWalkLeft()
    {
        float extraWidth = .05f;

        RaycastHit2D raycastHitWalk = Physics2D.Raycast(bc.bounds.center, Vector2.left, bc.bounds.extents.x + extraWidth, platformLayerMask);

        return raycastHitWalk.collider != null;
    }


    void FixedUpdate()
    {
        
    }
} 
