//The Grid class will also create sections but thats for later.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    protected enum Direction
    {
        North = 0, East, South, West
    };
    private int[] rotationArray = { 0, 90, 180, 270 };

    [Header("The obj requires the \"Tile.cs\"")]
    public GameObject obj;          //The object to use to make a grid with 
    public GameObject[] objArray;
    public List<GameObject> list;
    public int x, y;

    public float spread;

    [SerializeField]
    private GameObject exitTile;

    private bool exitTilePlaced;

    [SerializeField]
    private GameObject checkpointTile;

    [SerializeField]
    private int numberOfCheckpoints = 0;

    [SerializeField]
    private GameObject keyTile;

    protected GameObject[,] objArr;

    static int gridX, gridY;

    int keyCount;

    

    // Use this for initialization
    void Awake()
    {
        keyCount = GameManager.keys;

        if (x <= 0 || y <= 0)
            x = y = 1;
       // stack = new Stack<GameObject>();

        gridX = x;
        gridY = y;


        //if(obj != null)
        //{
        //    GenerateGridArray(obj);
        //}

        if (objArray != null)
        {
            GenerateGridArray(objArray); //Generates a grid using the 2d array
        }

        PathGenerator.GeneratePaths();

        //Generate Maze
        //Generate paths
        //Check if 

    }

    public static int GetGridSizeX
    {
        get { return gridX; }
    }

    public static int GetGridSizeY
    {
        get { return gridY; }
    }

    public GameObject CheckPlatformNorth(Vector2 position)
    {
        if (position.y < (y - 1))
            return objArr[(int)position.x, (int)position.y + 1];

        return null;
    }

    public GameObject CheckPlatformSouth(Vector2 position)
    {
        if (position.y > 0)
            return objArr[(int)position.x, (int)position.y - 1];

        return null;
    }

    public GameObject CheckPlatformEast(Vector2 position)
    {
        if (position.x < (x - 1))
            return objArr[(int)position.x + 1, (int)position.y];

        return null;
    }

    public GameObject CheckPlatformWest(Vector2 position)
    {
        if (position.x > 0)
            return objArr[(int)position.x - 1, (int)position.y];

        return null;
    }

    public bool OutOfBounds(Vector3 position)
    {
        if (position.x >= (x - 1) || position.x < 0 || position.z >= (y - 1) || position.z < 0)
            return true;
        return false;
    }

    protected void GenerateGridArray(GameObject tempObj)
    {
        objArr = new GameObject[x, y];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject temp = GameObject.Instantiate(tempObj);
                temp.name = "Platform " + i + "x" + j;

                temp.transform.parent = transform;
                temp.transform.position = new Vector3(i * spread, 0, j * spread);//new Vector3(i * temp.GetComponent<Renderer>().bounds.size.x, 0, j * temp.GetComponent<Renderer>().bounds.size.z);
                temp.GetComponent<Tile>().GenerateWeight();
                temp.GetComponent<Tile>().SetPosition(new Vector2(i, j));
                objArr[i, j] = temp;

            }
        }
    }

    protected void GenerateGridArray(GameObject[] tempObj)
    {
        objArr = new GameObject[x, y];
        Quaternion rot;
        float checkpointPercentage = 0f, exitPercentage = 0f, keyPercentage = 0f;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject temp = tempObj[Random.Range(0, tempObj.Length)];
                //temp.name = "Platform " + i + "x" + j;

                //if (Random.value <= checkpointPercentage && numberOfCheckpoints > 0)
                //{
                //    temp = checkpointTile;
                //    temp.name = "Checkpoint";
                //    checkpointPercentage = 0f;
                //    numberOfCheckpoints--;
                //}
                if(Random.value <= exitPercentage || i == x - 1 && j == y - 1)
                {
                    if (!exitTilePlaced)
                    {
                        temp = exitTile;
                        temp.name = "Exit Tile";
                        exitTilePlaced = true;
                    }
                }
                else if(Random.value <= keyPercentage && keyCount > 0)
                {
                    temp = keyTile;
                    temp.name = "Key Tile";
                    keyCount--;
                }
                else
                {
                    checkpointPercentage += 0.005f * (i + j) / 6;
                    exitPercentage += 0.0005f * (i + j) / 10; 
                    keyPercentage += 0.01f * (i + j) / 4;
                }
                rot = Quaternion.Euler(0, rotationArray[Random.Range(0, 4)], 0);
                temp = GameObject.Instantiate(temp);
                temp.transform.parent = transform;
                temp.transform.position = new Vector3(i * spread, 0, j * spread);
                temp.transform.rotation = rot;
                objArr[i, j] = temp;

            }
        }
    }
}