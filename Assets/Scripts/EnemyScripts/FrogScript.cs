using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator myAnimator;
    private bool animation_started;
    private bool animation_finished;

    private int jumpedTimes;
    private bool jumpLeft = true;

    private string coroutineName = "FrogJump";
    public LayerMask playerLayer;
    private GameObject player;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }
    void Start()
    {
        StartCoroutine(coroutineName);
    }
    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, .5f, playerLayer))
        {
            player.GetComponent<PlayerDamage>().DealDamage();
        }
    }
    void LateUpdate()
    {
        if (animation_finished && animation_started)
        {
            animation_started = false;
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }


    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        animation_started = true;
        animation_finished = false;

        jumpedTimes++;

        if(jumpLeft)
        {
            myAnimator.Play("FrogJumpLeft");
        }
        else
        {
            myAnimator.Play("FrogJumpRight");
        }
        StartCoroutine(coroutineName);
    }

    void AnimationFinished()
    {
        animation_finished = true;

        if (jumpLeft) { 
            myAnimator.Play("FrogIdleLeft");
        }
        else { 
            myAnimator.Play("FrogIdleRight");
        }

        if (jumpedTimes == 3)
        {
            jumpedTimes = 0;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft;
            
        }
    }
}
