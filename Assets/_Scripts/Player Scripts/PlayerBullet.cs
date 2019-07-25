using UnityEngine;

public class PlayerBullet : Bullet
{
    public void IntializeBullet(Vector3 direction, float speed, Vector3 targetPosition)
    {
        this.direction = direction;
        this.speed = speed;

        if (targetPosition != Vector3.zero)
        {
            this.direction = Vector3.Normalize(targetPosition - transform.position);
        }

        //base.IntializeBullet(direction, speed);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag != "Player" && other.transform.tag != "Node")
        {
            //Debug.Log("Bullet hit " + other.name);
            if (other.transform.tag == "Enemy")
            {
                other.transform.GetComponent<EnemyHealth>().DealDamage(100, GameObject.FindGameObjectWithTag("Player").transform);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }


    }
}
