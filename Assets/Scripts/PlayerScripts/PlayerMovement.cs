using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D myBody;
    private Animator myAnimator;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    [SerializeField]
    private float jumpSpeed = 12f;
    private bool onGround;
    private bool jumping;
   
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckOnGround();
        PlayerJump();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h>0)
        {
            // Move player to the right
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            SetDirection(1);
        }
        else if(h<0)
        {
            // Move player to the left
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            SetDirection(-1);
        }
        else
        {
            // Stop the player
            myBody.velocity = new Vector2(0, myBody.velocity.y);
        }
        myAnimator.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }
    void SetDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
    private void CheckOnGround()
    {
        onGround = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
        if (onGround)
        {
            if(jumping)
            {
                jumping = false;
                myAnimator.SetBool("Jump", false);
            }
        }
    }
    private void PlayerJump()
    {
        if (onGround)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                jumping = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpSpeed);
                myAnimator.SetBool("Jump", true);
            }
        }
    }
} // End Class

