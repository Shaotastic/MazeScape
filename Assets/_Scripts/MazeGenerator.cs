using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : GridGenerator
{
    public Stack<GameObject> stack;

    // Use this for initialization
    void Start()
    {
        stack = new Stack<GameObject>();
        GenerateGridArray(obj);
        GenerateMaze(objArr[0,0]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateMaze(GameObject objt)
    {

        if (!objt.GetComponent<Tile>().visited)
        {
            stack.Push(objt);
            objt.GetComponent<Tile>().visited = true;
        }

        if (stack.Count != 0)
        {
            GameObject n = CheckPlatformNorth(stack.Peek().GetComponent<Tile>().tilePosition),
                       s = CheckPlatformSouth(stack.Peek().GetComponent<Tile>().tilePosition),
                       e = CheckPlatformEast(stack.Peek().GetComponent<Tile>().tilePosition),
                       w = CheckPlatformWest(stack.Peek().GetComponent<Tile>().tilePosition);

            ArrayList weight = new ArrayList();


            if (n && !n.GetComponent<Tile>().visited)
                weight.Add(n);
            if (e && !e.GetComponent<Tile>().visited)
                weight.Add(e);
            if (s && !s.GetComponent<Tile>().visited)
                weight.Add(s);
            if (w && !w.GetComponent<Tile>().visited)
                weight.Add(w);

            if (weight.Capacity != 0)
            {
                GameObject x = GetLowestValue(weight);
                DisableWall(x.GetComponent<Tile>(), stack.Peek().GetComponent<Tile>());
                GenerateMaze(x);
            }
            else
                GenerateMaze(stack.Pop());
        }
    }
    GameObject GetLowestValue(ArrayList list)
    {
        GameObject lowest = null;
        int value = 0;
        foreach (GameObject n in list)
        {
            if (value == 0)
            {
                value = n.GetComponent<Tile>().GetWeight();
                lowest = n;
            }
            else
            {
                if (n.GetComponent<Tile>().GetWeight() < lowest.GetComponent<Tile>().GetWeight())
                {
                    value = n.GetComponent<Tile>().GetWeight();
                    lowest = n;
                }
            }
        }
        return lowest;
    }

    void DisableWall(Tile a, Tile b)
    {
        if (CheckDirection(a.tilePosition, b.tilePosition) == Direction.North)
        {
            a.SetSouthWall(false);
            b.SetNorthWall(false);
        }
        else if (CheckDirection(a.tilePosition, b.tilePosition) == Direction.East)
        {
            a.SetWestWall(false);
            b.SetEastWall(false);
        }
        else if (CheckDirection(a.tilePosition, b.tilePosition) == Direction.South)
        {
            a.SetNorthWall(false);
            b.SetSouthWall(false);
        }
        else
        {
            a.SetEastWall(false);
            b.SetWestWall(false);
        }
    }

    Direction CheckDirection(Vector2 a, Vector2 b)
    {
        Vector2 direction = a - b;

        if (direction.y > 0)
            return Direction.North;

        else if (direction.x > 0)
            return Direction.East;

        else if (direction.y < 0)
            return Direction.South;

        else// if (direction.x < 0)
            return Direction.West;
    }
}
