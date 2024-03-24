using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlockScript : MonoBehaviour
{
    public Transform bottomCollision;
    public LayerMask playerLayer;

    private Animator myAnimator;

    private Vector3 moveDirection = Vector3.up;
    private Vector3 originPosition;
    private Vector3 animPosition;
    private bool hitable;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        hitable = true;
    }
    void Start()
    {
        originPosition = transform.position;
    }

    void Update()
    {
        checkForCollision();
    }

    private void checkForCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f, playerLayer);
        if (hit) { 
            if(hit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if(hitable)
                {
                    hitable = false;
                    myAnimator.Play("BonusCollected");
                }
            }
        }
    }





































} // End Class

