using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    protected float health;

    private float startingHealth;

    public Health()
    {
       health = 100f;
        startingHealth = health;
    }
    
    public Health(float healthAmount)
    {
        health = healthAmount;
        startingHealth = health;
    }

    public float GetHealth
    {
        get { return Mathf.Round(health); }
    }

    protected float getHealth
    {
        get { return health; }
    }

    public float GetStartingHealth
    {
        get { return startingHealth; }
    }

    public virtual void DealDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    public void SetStartingHealth()
    {
        startingHealth = health;
    }

    public void ResetHealth()
    {
        health = startingHealth;
    }

    protected virtual void Dead()
    {
        Destroy(this.gameObject);
    }
}
