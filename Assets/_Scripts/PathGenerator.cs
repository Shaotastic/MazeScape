using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{

    public static Node[] nodes;

    // Use this for initialization
    void Start()
    {
        if(nodes != null)
        {
            nodes = null;
        }
        nodes = GameObject.FindObjectsOfType<Node>();
        RaycastHit ray;
        for (int i = 0; i < nodes.Length; i++)
        {
            for (int j = 0; j < nodes.Length; j++)
            {
                //if (Vector3.Distance(nodes[i].transform.position, nodes[j].transform.position) <= 5.5f)
                //{
                if (i != j)
                {
                    Vector3 direction = Vector3.Normalize(nodes[j].transform.position - nodes[i].transform.position);
                    if (Physics.Raycast(nodes[i].transform.position, direction, out ray, Mathf.Infinity))
                    {
                        float distance = Vector3.Distance(nodes[i].transform.position, nodes[j].transform.position);
                        if (ray.transform.tag == "Node" &&  distance <= 8f)
                            nodes[i].AddNeighborNode(nodes[j], distance);
                    }
                }
                //}
            }

        }
    }

    public static Node FindNode(Vector3 position)
    {
        Node node = null;

        for (int i = 0; i < nodes.Length; i++)
        {
            if (Vector3.Distance(position, nodes[i].transform.position) <= 5)
            {
                node = nodes[i];
                return node;
            }
        }

        return node;
    }

}
