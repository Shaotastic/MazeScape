using UnityEngine;

public abstract class State
{
    protected Transform transform;
    protected GameObject gameObject;
    protected Transform target;
    protected StateManager stateManager;
    public State(GameObject obj)
    {
        this.gameObject = obj;
        this.transform = obj.transform;
        stateManager = obj.GetComponent<StateManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public abstract void Tick();

    public abstract void Transition();

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    protected bool EnemyInSight(Vector3 direction, float fieldOfView, float sightRange)
    {
        float inSight = Vector3.Dot(direction.normalized, transform.forward);
        float angle = Mathf.Acos(fieldOfView) * Mathf.Rad2Deg;


        Vector3 lineA = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward.normalized * sightRange;// Mathf.Cos(angle) * transform.position.normalized;
        Vector3 lineB = Quaternion.AngleAxis(-angle, Vector3.up) * transform.forward.normalized * sightRange;

        Debug.DrawLine(transform.position, (transform.position + lineA), Color.white);
        Debug.DrawLine(transform.position, (transform.position + lineB), Color.white);
        //Debug.DrawLine((transform.position + lineA), (transform.position + lineB), Color.white);

        RaycastHit hit;

        if (direction.magnitude <= sightRange)
        {
            if (Physics.Raycast(transform.position, direction, out hit) && hit.collider.tag == "Player" && inSight >= fieldOfView)
                return true;
        }

        return false;
    }

}
