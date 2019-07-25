using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletImpact;

    protected Vector3 direction;
    protected float speed;
    protected float time = 2;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(this.gameObject, time);
    }

    public virtual void IntializeBullet(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log(direction);
        transform.position += direction * Time.deltaTime * speed;
    }

    protected virtual void OnDestroy()
    {
        if(bulletImpact)
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
    }
}

