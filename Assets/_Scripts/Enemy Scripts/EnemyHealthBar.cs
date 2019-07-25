using UnityEngine;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour
{

    [SerializeField]
    private UnityEngine.UI.Image healthBar;

    [SerializeField]
    private Health health;

    Vector3 position;
    // Use this for initialization
    void Start()
    {
        position = transform.localPosition;
        health = GetComponentInParent<Health>();
        healthBar = transform.Find("Foreground").GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health.GetHealth / health.GetStartingHealth;
        
    }

    void FixedUpdate()
    {
        transform.position = transform.parent.position + position;
        transform.LookAt(Camera.main.transform.position);
    }
}
