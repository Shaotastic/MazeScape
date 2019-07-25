using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Image playerHealth;

    Health health;
    // Use this for initialization
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.fillAmount = health.GetHealth / health.GetStartingHealth;
    }
}
