using UnityEngine;

public class Drone : MonoBehaviour
{

    public StateManager states;
    public GameObject bullet;
    Vector3 direction;
    Vector3 influenceDirection;

    float distance = 3;

    public float accel;
    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        if(!GetComponent<StateManager>())
        {
            gameObject.AddComponent<StateManager>();
        }
        states = GetComponent<StateManager>();
        rigid = GetComponent<Rigidbody>();
        IntializeStates();
    }

    void IntializeStates()
    {
        states.AddState(new WanderState(this.gameObject));
        states.AddState(new AttackState(this.gameObject, bullet));
    }

    private void FixedUpdate()
    {
        // rigid.AddForce(Vector3.up * 2);
        //rigid.AddForce(Vector3.up * accel, ForceMode.Force);
    }

}
