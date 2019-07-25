using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    Vector3 direction;
    Vector3 currentDirection, influenceDirection;

    float distance = 2;
    float timer, maxTime = 3;
    float firedTimer, fireRateTime = 1;
    private bool findPlayer;
    bool lost;
    bool fired;
    GameObject bullet;
    public AttackState(GameObject obj, GameObject bullet) : base(obj)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        this.bullet = bullet;
    }

    

    public override void Tick()
    {
        DelayFiredBullet();

        if(findPlayer)
        {
            timer += Time.deltaTime;
            if(timer >= maxTime)
            {
                lost = true;
            }
        }

        direction = target.position - transform.position;
        //throw new System.NotImplementedException();

        if (Vector3.Distance(transform.position, target.position) >= 7)
            transform.position += transform.forward * Time.deltaTime * 3;
        if(Vector3.Distance(transform.position, target.position) <= 12 && EnemyInSight(direction, 0.99f, 12))
            FireBullet();



        if (!Sensors())
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(influenceDirection), Time.deltaTime * 8);

        //RaycastToPlayer();
    }

    public override void Transition()
    {

        if(lost)
            stateManager.TransitionState(typeof(WanderState));
    }

    void RaycastToPlayer()
    {
        RaycastHit hit;

        //throw new System.NotImplementedException();
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag != "Player")
            {

                Debug.Log("Found player");
                findPlayer = true;
            }
            else
            {
                Debug.Log("Losing player");
                findPlayer = false;
            }
        }
    }

    void FireBullet()
    {
        Debug.Log("Firing bullet");
        if(!fired)
        {
            var unused = Object.Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            unused.GetComponent<EnemyBullet>().IntializeBullet(target.position - transform.position, 30);
            fired = true;
        }

    }

    void DelayFiredBullet()
    {
        if(fired)
        {
            firedTimer += Time.deltaTime;
            if (firedTimer >= fireRateTime)
            {
                fired = false;
                firedTimer = 0;
            }
        }
    }

    bool Sensors()
    {
        RaycastHit hit;
        influenceDirection = Vector3.zero;
        for (int i = 0; i < 11; i++)
        {
            Quaternion rot = Quaternion.Euler(0, -50 + (i * 10), 0);
            Vector3 direction = rot * transform.forward;
            if (Physics.Raycast(transform.position, direction, out hit, distance))
            {
                if (hit.transform.tag != "Node" && hit.transform.tag != "Bullet")
                {
                    Debug.DrawRay(transform.position, direction * distance, Color.green);

                    //if (direction.y % 90 != 0)
                    //Debug.DrawRay(transform.position, direction * distance, Color.blue);
                    Debug.Log(hit.transform.name);
                    influenceDirection = Vector3.Reflect(-direction, transform.forward);// new Vector3(0, -direction.y, 0);
                    return true;
                }
                else
                {
                    Debug.DrawRay(transform.position, direction * distance, Color.yellow);
                }
            }
            else
                Debug.DrawRay(transform.position, direction * distance, Color.yellow);

        }

        return false;
    }
}
