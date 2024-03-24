using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject stone;
    public Transform stoneLocation;
    private Animator myAnimator;
    private string coroutine_name = "StartAttack";

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
   
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(coroutine_name);
    }

    void Attack()
    {
        GameObject obj = Instantiate(stone, stoneLocation.position, Quaternion.identity);   
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 100));
    }

    void BackToIdle()
    {
        myAnimator.Play("BossIdle");
    }
    public void DeactivateBossScript()
    {
        StopCoroutine(coroutine_name);
        enabled = false;
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        myAnimator.Play("BossAttackAnimation");
        StartCoroutine(coroutine_name);
    }   
















} // End Class
