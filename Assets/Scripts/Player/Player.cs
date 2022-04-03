using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public StateMachine stateMachine;

    public PlayerMovingState movingState;


    void Start()
    {
        stateMachine = new StateMachine();
        movingState = new PlayerMovingState(this);

        stateMachine.ChangeState(movingState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
