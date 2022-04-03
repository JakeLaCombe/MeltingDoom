using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnState : IState
{
    private Player player;

    private Rigidbody2D rigidBody;
    private IInputable input;

    private Vector3 spawnPoint;

    public PlayerRespawnState(Player player)
    {
        this.player = player;
        rigidBody = this.player.GetComponent<Rigidbody2D>();
        input = this.player.GetComponent<IInputable>();
    }
    // Start is called before the first frame update
    public void Enter()
    {
        this.player.StartCoroutine(startRespawn());
    }
    public void Execute()
    {

    }
    public void Exit() { }

    public void SetSpawnPoint(Vector3 vector)
    {
        Debug.Log("Spawn Point");
        Debug.Log(vector);
        spawnPoint = vector;
    }

    public IEnumerator startRespawn()
    {
        yield return new WaitForSeconds(5.0f);
        player.transform.position = spawnPoint;
        Debug.Log("Spawned");
        Debug.Log(spawnPoint);
        player.activatePlayerState();
    }
    public void OnTriggerEnter2D(Collider2D other) { }
}
