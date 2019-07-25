using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool outOfSight;

    [SerializeField]
    private bool attacking, attack;

    private Rigidbody rigid;

    //EnemyHealth healthClass;

    LineRenderer line;

    public Node node, prevNode;
    bool canMove = true;

    // Use this for initialization
    void Start()
    {
        // healthClass = GetComponent<EnemyHealth>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        // node = PathGenerator.FindNode(transform.position);
        // prevNode = node;
    }

    // Update is called once per frame
    void Update()
    {
        if (node == null)
        {
            node = PathGenerator.FindNode(transform.position);
        }
        else
        {
            if (Vector3.Distance(transform.position, node.transform.position) < 2)
            {
                //set prevNode
                prevNode = node;

                //Select the next node
                node = node.SelectNeighborNode(prevNode);
            }
            //else if (Vector3.Distance(transform.position, node.transform.position) > 5 && outOfSight)
            //{
            //    node = PathGenerator.FindNode(transform.position);
            //}
            else
            {
                if (canMove && outOfSight)
                    rigid.AddForce(Vector3.Normalize(node.transform.position - transform.position) * 2, ForceMode.Impulse);
                else if (!outOfSight && rigid.velocity.y >= -1.5f)
                    rigid.AddForce(target.position - transform.position, ForceMode.Impulse);

            }
        }

        if (Vector3.Distance(transform.position, target.position) < 5)
            attack = true;
        else if (Vector3.Distance(transform.position, target.position) > 7.5f)
            attack = false;



        if (attack && !outOfSight)
            Attack();
        else
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
        }
    }

    void FixedUpdate()
    {
        RaycastHit ra;
        if (Physics.Raycast(transform.position, target.position - transform.position, out ra) && ra.collider.tag == "Player")
        {
            outOfSight = false;
        }
        else
        {

            outOfSight = true;
        }
    }

    void Feet()
    {

    }

    void Attack()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target.transform.position);
        target.GetComponent<PlayerHealth>().DealDamage(0.5f);


    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.contactCount);
        if (col.collider.tag == "Player")
        {
            if (col.relativeVelocity.magnitude > 5)
            {
                float dam = (col.relativeVelocity.magnitude * 10);
                Debug.Log("Magnitude: " + col.relativeVelocity.magnitude + ", Damage: " + dam);
                target.GetComponent<PlayerHealth>().DealDamage(dam);
            }
        }
        else if (col.collider.tag == "Enemy")
        {
            if (col.relativeVelocity.magnitude > 5)
            {
                float dam = (col.relativeVelocity.magnitude * 10);
                Debug.Log("Magnitude: " + col.relativeVelocity.magnitude + ", Damage: " + dam);
                col.transform.GetComponent<EnemyHealth>().DealDamage(dam);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        canMove = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canMove = false;
    }
}
