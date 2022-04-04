using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public StateMachine stateMachine;

    public PlayerMovingState movingState;
    public PlayerRespawnState respawnState;
    public PlayerDeadState deadState;


    void Start()
    {
        stateMachine = new StateMachine();
        movingState = new PlayerMovingState(this);
        respawnState = new PlayerRespawnState(this);
        deadState = new PlayerDeadState(this);

        stateMachine.ChangeState(movingState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public void respawnPlayer(Vector3 vector)
    {
        if (stateMachine.currentState != respawnState)
        {
            respawnState.SetSpawnPoint(vector);
            stateMachine.ChangeState(respawnState);
        }
    }

    public void activatePlayerState()
    {
        stateMachine.ChangeState(movingState);
    }

    public void melt()
    {
        stateMachine.ChangeState(deadState);
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        stateMachine.currentState.OnTriggerEnter2D(collider2D);
    }
}
