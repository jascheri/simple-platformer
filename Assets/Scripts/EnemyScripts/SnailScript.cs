using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private Rigidbody2D myBody;
    private Animator myAnimator;

    public LayerMask playerLayer;

    private bool moveLeft;

    public LayerMask layerTarget;

    private bool canMove;
    private bool stunned;

    public Transform collision_left, collision_right, collision_down, collision_up;
    private Vector3 leftCollisionPosition, rightCollisionPosition;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        leftCollisionPosition = collision_left.position;
        rightCollisionPosition = collision_right.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    public void Update()
    {
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }
        checkCollision();
    }
    private void checkCollision() {
        RaycastHit2D leftHit = Physics2D.Raycast(collision_left.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(collision_right.position, Vector2.right, 0.1f, playerLayer);
        Collider2D topHit = Physics2D.OverlapCircle(collision_up.position, 0.2f, playerLayer);

        if(topHit != null) {
            if(topHit.gameObject.tag == MyTags.PLAYER_TAG) {
                if(!stunned) {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    myAnimator.Play("Stunned");
                    stunned = true;

                    // BEETLE CODE HERE
                    if(gameObject.tag == MyTags.BEETLE_TAG)
                    {
                        StartCoroutine(Dead(.5f));
                    }
                }
            }
        }
        if (leftHit) {
            if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG) {
                if (!stunned) {
                    // Damage Player
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                } else {
                    // Push the snail
                    if(tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(15.0f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }
        if (rightHit) {
            if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG) {
                if (!stunned) {
                    // Damage Player
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                } else {
                    // Push the snail
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(-15.0f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if (!Physics2D.Raycast(collision_down.position, Vector2.down, 0.1f, layerTarget))
        {
                changeDirection();
        }
    }
    private void changeDirection() {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;
        if (moveLeft) {
            tempScale.x = Mathf.Abs(tempScale.x);
            collision_left.position = leftCollisionPosition;
            collision_right.position = rightCollisionPosition;
        } else {
            tempScale.x = -Mathf.Abs(tempScale.x);
            collision_left.position = rightCollisionPosition;
            collision_right.position = leftCollisionPosition;
        }
        transform.localScale = tempScale;
    }
    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.BULLET_TAG)
        {
            if(tag == MyTags.BEETLE_TAG)
            {
                myAnimator.Play("Stunned");
                canMove = false;
                myBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
            }
            if(tag == MyTags.SNAIL_TAG)
            {
                if(!stunned)
                {
                    myAnimator.Play("Stunned");
                    stunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
} // end of class
