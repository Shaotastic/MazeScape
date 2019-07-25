using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{

    const int size = 4;
    [Header("0 = Top, 1 = Right, 2 = Bottom, 3 = Left")]
    [Header("Which side of tile has a wall.")]
    public bool[] walls = new bool[size];
    public GameObject northWall, eastWall, southWall, westWall;
    public GameObject floor;
    public int weight;



    Vector2 position;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnValidate()
    {
        if (walls.Length != size)
            Debug.LogError("The array size for 'walls' array MUST be size of 4!");
    }

    public int GetWeight()
    {
        return weight;
    }

    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }

    public Vector2 tilePosition
    {
        get { return position; }
    }

    public void SetNorthWall(bool x)
    {
        if (x)
            northWall.SetActive(true);
        else
            northWall.SetActive(false);
    }

    public void SetEastWall(bool x)
    {
        if (x)
            eastWall.SetActive(true);
        else
            eastWall.SetActive(false);
    }

    public void SetSouthWall(bool x)
    {
        if (x)
            southWall.SetActive(true);
        else
            southWall.SetActive(false);
    }

    public void SetWestWall(bool x)
    {
        if (x)
            westWall.SetActive(true);
        else
            westWall.SetActive(false);
    }

    public void GenerateWeight()
    {
        weight = Random.Range(1, 6);

        //switch (weight)
        //{
        //    case 1:
        //        floor.GetComponent<MeshRenderer>().material.color = Color.magenta;
        //        break;
        //    case 2:
        //        floor.GetComponent<MeshRenderer>().material.color = Color.blue;
        //        break;
        //    case 3:
        //        floor.GetComponent<MeshRenderer>().material.color = Color.green;
        //        break;
        //    case 4:
        //        floor.GetComponent<MeshRenderer>().material.color = Color.yellow;
        //        break;
        //    case 5:
        //        floor.GetComponent<MeshRenderer>().material.color = Color.red;
        //        break;
        //    default:
        //        break;
        //}
    }

    public bool visited;

}
