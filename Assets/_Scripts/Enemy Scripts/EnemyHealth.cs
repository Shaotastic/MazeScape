using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public GameObject Explosion;
    Transform target;
    bool damaged;

    void Start()
    {
        SetStartingHealth();
    }

    public void DealDamage(float damageAmount, Transform attacker)
    {
        health -= damageAmount;
        if (health <= 0)
            Dead();
        target = attacker;
        StartCoroutine(Damaged());

    }

    protected override void Dead()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        GetComponent<MeshRenderer>().enabled = false;
        GameManager.enemiesKilled++;
        //GetComponent<Ball>().enabled = false;
        //GetComponent<StateManager>().enabled = false;
        //GetComponent<Enemy>().enabled = false;
        Destroy(this.gameObject, 2);
    }

    public void OnDestroy()
    {

    }

    IEnumerator Damaged()
    {
        damaged = true;
        yield return new WaitForSeconds(0.5f);
        damaged = false;
    }

    public bool IsDamaged
    {
        get {
            return damaged;
        }
    }

    public Transform Attacker
    {
        get {
            return target;
        }
    }
}
