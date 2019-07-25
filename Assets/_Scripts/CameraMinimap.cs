using UnityEngine;
using System.Collections;

public class CameraMinimap : MonoBehaviour
{
    [SerializeField]
    Light light;

    [SerializeField]
    float height;
    [SerializeField]
    Transform target;

    new Camera camera;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camera.transform.position = new Vector3(target.position.x, 20, target.position.z); 
        //camera.transform.rotation = Qua
    }

    void OnPreCull()
    {
        light.enabled = false;
    }

    void OnPostRender()
    {
        light.enabled = true;
    }
}
