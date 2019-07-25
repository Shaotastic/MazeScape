using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public StateManager states;
    // Start is called before the first frame update
    void Start()
    {
        states = GetComponent<StateManager>();

        states.AddState(new WanderState(this.gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
