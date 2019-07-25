using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    #region Variables
    public List<Node> NeighborNodes;
    public List<float> nodeScores;

    #endregion

    void Update()
    {
        if(NeighborNodes.Count != 0)
        {
            foreach(Node node in NeighborNodes)
            {
                Debug.DrawLine(transform.position, node.transform.position, Color.red);
            }
        }
    }

    public void AddNeighborNode(Node node)
    {
        if (NeighborNodes.Count != 0)
        {
            foreach (Node currentNode in NeighborNodes)
            {
                if (currentNode != node)
                {
                    NeighborNodes.Add(node);
                    break;
                }
            }
        }
        else
            NeighborNodes.Add(node);
    }

    public void AddNeighborNode(Node node, float score)
    {
        if (NeighborNodes.Count != 0)
        {
            foreach (Node currentNode in NeighborNodes)
            {
                if (currentNode != node)
                {
                    NeighborNodes.Add(node);
                    nodeScores.Add(score);
                    break;
                }
            }
        }
        else
        {
            NeighborNodes.Add(node);
            nodeScores.Add(score);
        }
    }

    public Node SelectNeighborNode(Node prevNode)
    {
        Node node;
        while(true){
            node = NeighborNodes[Random.Range(0, NeighborNodes.Count)];
            if (node != prevNode)
                break;
        }

        return node;
    }
}
