using UnityEngine;

public class PlayerMarker : Bullet
{
    bool stopMovement;

    protected override void Start()
    {
        stopMovement = false;
    }

    public void IntializeBullet(Vector3 direction, float speed, Vector3 targetPosition)
    {
        this.direction = direction;
        this.speed = speed;

        if (targetPosition != Vector3.zero)
        {
            this.direction = Vector3.Normalize(targetPosition - transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag != "Player" || other.transform.tag != "Node")
        {
            stopMovement = true;
        }
    }

    protected override void Update()
    {
        if(!stopMovement)
            transform.position += direction * Time.deltaTime * speed;        
    }
}
