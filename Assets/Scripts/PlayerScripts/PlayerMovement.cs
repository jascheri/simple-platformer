using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D myBody;
    private Animator myAnimator;

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

    }
    private void FixedUpdate()
    {
        movePlayer();
    }
    
    private void movePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h>0)
        {
            // Move player to the right
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            setDirection(1);
        }
        else if(h<0)
        {
            // Move player to the left
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            setDirection(-1);
        }
        else
        {
            // Stop the player
            myBody.velocity = new Vector2(0, myBody.velocity.y);
        }
        myAnimator.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }
    void setDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
} // End Class

