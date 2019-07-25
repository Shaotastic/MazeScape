using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    [SerializeField] List<State> stateList = new List<State>();
    [SerializeField] Dictionary<Type, State> pairs = new Dictionary<Type, State>();

    [SerializeField] State currentState;

    // Update is called once per frame
    public void AddState(State state)
    {
        //stateList.Add(state);
        pairs.Add(state.GetType(), state);
    }

    private void Update()
    {
        if (currentState == null && pairs.Count != 0)
            currentState = pairs.Values.First();

        if (currentState != null)
        {
            currentState.Tick();
            currentState.Transition();
        }
    }

    public void TransitionState(Type state)
    {
        currentState = pairs[state];
    }
}
