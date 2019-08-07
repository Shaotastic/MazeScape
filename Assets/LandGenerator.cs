using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGenerator : MonoBehaviour
{
    Terrain terrain;
    MeshFilter mesh;
    Vector3[] vertices;
    public int resX;
    public int resY;
    public float[,] heights;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        //mesh = GetComponent<MeshFilter>();
        resX = terrain.terrainData.heightmapWidth;
        resY = terrain.terrainData.heightmapHeight;

        heights = terrain.terrainData.GetHeights(0, 0, resX, resY);

        for(int i = 0; i < resX; i++)
        {
           for(int j = 0; j < resY; j++)
            {
                heights[i, j] = Mathf.PerlinNoise(Random.Range(0,10) / resX , Random.Range(0, 10) / resY);

                Debug.Log(heights[i, j]);
            }
        }
        //terrain.terrainData.SetHeights(0, 0, heights);
        //mesh.mesh.vertices = vertices;
    }

    // Update is called once per frame
    void Update()
    {
            
    }


}
