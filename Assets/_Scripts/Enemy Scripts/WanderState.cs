using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    EnemyHealth health;
    public WanderState(GameObject obj) : base(obj)
    {
        health = obj.GetComponent<EnemyHealth>();
    }

    public Node node, prevNode;
    Vector3 currentDirection, influenceDirection;

    float distance = 2;
    private float m_FieldOfView;
    private float m_SightRange;

    public override void Tick()
    {
        if (node == null)
        {
            node = PathGenerator.FindNode(transform.position);
        }
        else
        {
            if (Vector3.Distance(transform.position, node.transform.position) <= 2)
            {
                //set prevNode
                //prevNode = node;

                //Select the next node
                node = node.SelectNeighborNode(prevNode);
            }
            else
            {
                currentDirection = Vector3.Normalize(node.transform.position - transform.position);
                transform.position += transform.forward * Time.deltaTime * 3;
                //transform.Translate(transform.forward * Time.deltaTime * 3);

                if (!Sensors())
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentDirection), Time.deltaTime * 3);
                else
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(influenceDirection), Time.deltaTime * 4);

            }
        }

        // Sensors();

    }

    public override void Transition()
    {
        if(health.IsDamaged || EnemyInSight(currentDirection, 0.70f, 10.0f))
        {
            stateManager.TransitionState(typeof(AttackState));         
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
                if (hit.transform.tag != "Node" || hit.transform.tag != "Bullet")
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

    //private bool EnemyInSight(Vector3 direction, float fieldOfView, float sightRange)
    //{
    //    float inSight = Vector3.Dot(currentDirection.normalized, transform.forward);
    //    float angle = Mathf.Acos(fieldOfView) * Mathf.Rad2Deg;


    //    Vector3 lineA = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward.normalized * sightRange;// Mathf.Cos(angle) * transform.position.normalized;
    //    Vector3 lineB = Quaternion.AngleAxis(-angle, Vector3.up) * transform.forward.normalized * sightRange;

    //    Debug.DrawLine(transform.position, (transform.position + lineA), Color.white);
    //    Debug.DrawLine(transform.position, (transform.position + lineB), Color.white);
    //    //Debug.DrawLine((transform.position + lineA), (transform.position + lineB), Color.white);

    //    RaycastHit hit;

    //    if (currentDirection.magnitude <= sightRange)
    //    {
    //        if (Physics.Raycast(transform.position, currentDirection, out hit) && hit.collider.tag == "Player" && inSight >= fieldOfView)
    //            return true;        
    //    }
     
    //    return false;
    //}
}
