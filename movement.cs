using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    //declare variables
    private float jumpforce=70f;
    private float speed=30f;
    public float movex;
    private float jumptime=.22f;
    public float jumpcount;
    public float checkradius;
    private float fallacc=40;
    
    public bool isjumping;
    public bool bumphead;

    public Transform feet;
    public Transform head;
    private Rigidbody2D rb;
    Vector2 grav;


    private bool facingright = true;
    public bool grounded;

    public LayerMask groundlayer;

    // Start is called before the first frame update
    void Start()
    {
        grav = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        checkground();
        checkhead();
    }

    private void checkground()
    {
        grounded = Physics2D.OverlapCircle(feet.position,checkradius,groundlayer);
        bumphead = Physics2D.OverlapCircle(head.position, checkradius, groundlayer);
    }

    private void checkhead()
    {
        bumphead = Physics2D.OverlapCircle(head.position, checkradius, groundlayer);
    }

    private void physics()
    {
        transform.position += new Vector3(movex, 0,0) * Time.deltaTime * speed;
    }

    private void xmove()
    {
        movex=Input.GetAxis("Horizontal");
    }

    private void move()
    {
        xmove();
        jump();
        direction();
        physics();
    }

    private void direction()
    {
        if (movex > 0 && !facingright)
            flip();

        if (movex < 0 && facingright)
            flip();
    }

    private void jump()
    {
        if ((Input.GetButtonDown("Jump") && grounded == true))
        {
            isjumping = true;
            jumpcount = jumptime;
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);

        }

        if ((Input.GetKey(KeyCode.Space) && isjumping == true))
        {
            if (jumpcount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                jumpcount -= Time.deltaTime;
            }
            else{
                isjumping = false;
            }
        }

        if ((Input.GetKeyUp(KeyCode.Space))){
            isjumping = false;
          }  
        if (bumphead==true)
        {
            isjumping = false;
        }
        if (isjumping==false)
            rb.velocity -= grav * fallacc * Time.deltaTime;
    }
    private void flip() {
        facingright = !facingright;
        transform.Rotate(0f, 180f, 0);
    }
}
