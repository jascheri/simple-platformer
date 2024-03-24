using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{

    private Text lifeText;
    private int lifeCount;
    private bool canDamage;

    // Start is called before the first frame update
    void Start()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeCount = 3;
        lifeText.text = "x" + lifeCount.ToString();

        canDamage = true;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    public void DealDamage()
    {
        if (canDamage)
        {
            canDamage = false;
            lifeCount--;
            if(lifeCount >= 0)
            {
                lifeText.text = "x" + lifeCount.ToString();
            }
            if (lifeCount == 0)
            {
                // Restart Game
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("MainMenu");
    }

} // End Class
