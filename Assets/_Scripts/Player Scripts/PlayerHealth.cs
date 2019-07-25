using UnityEngine;
using System.Collections;

public class PlayerHealth : Health
{
    [SerializeField]
    Vector3 checkpoint;


    void Start()
    {
        SetStartingHealth();
        checkpoint = transform.position;
    }

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if (col.collider.tag == "Checkpoint")
        {
            checkpoint = col.gameObject.transform.position;
            Debug.Log("Checkpoint reached!");
        }
    }

    public override void DealDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
            Dead();
    }

    protected override void Dead()
    {
        ResetHealth();
        transform.position = checkpoint;
        GameManager.playerDeaths++;
    }

}
