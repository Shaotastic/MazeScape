using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Image playerHealth;

    [SerializeField] Text scoreText;

    Health health;
    // Use this for initialization
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        if (scoreText == null)
            Debug.LogWarning("Score Text object is not available, score cannout be displayed");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health != null)
            playerHealth.fillAmount = health.GetHealth / health.GetStartingHealth;

        if (scoreText != null)
        {
            scoreText.text = GameManager.GetKeyAmount() + " / " + GameManager.keys;
        }



    }
}
