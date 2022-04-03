using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public StateMachine stateMachine;

    public PlayerMovingState movingState;
    public PlayerRespawnState respawnState;


    void Start()
    {
        stateMachine = new StateMachine();
        movingState = new PlayerMovingState(this);
        respawnState = new PlayerRespawnState(this);

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
            Debug.Log(vector);
            respawnState.SetSpawnPoint(vector);
            stateMachine.ChangeState(respawnState);
        }
    }

    public void activatePlayerState()
    {
        stateMachine.ChangeState(movingState);
    }
}
