using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text coinScoreText;
    private AudioSource audioManager;
    private int scoreCount;
    private void Awake()
    {
        coinScoreText = GameObject.Find("CoinText").GetComponent<Text>();
        audioManager = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        coinScoreText.text = "x0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.COIN_TAG)
        {
            collision.gameObject.SetActive(false);
            scoreCount++;

            coinScoreText.text = "x" + scoreCount.ToString();
            audioManager.Play();
        }
    }
} // End Class

