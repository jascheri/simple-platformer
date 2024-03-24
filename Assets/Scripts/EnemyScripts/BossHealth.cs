using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private Animator myAnimator;
    private int health = 10;

    private bool canDamage;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        canDamage = true;
    }    

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (canDamage)
        {
            if (target.tag == MyTags.BULLET_TAG)
            {
                health--;
                canDamage = false;
                if (health == 0)
                {
                    GetComponent<BossScript>().DeactivateBossScript();
                    myAnimator.Play("BossDead");
                }
                StartCoroutine(WaitForDamage());
            }
        }
    }











} // End Class
