using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator myAnimator;

    private bool canMove;
    // Start is called before the first frame update
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if(canMove)
        { 
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BEETLE_TAG || collision.tag == MyTags.SNAIL_TAG)
        {
           myAnimator.Play("Explode");
            canMove = false;
           StartCoroutine(DisableBullet(0.2f));
        }
    }
}
