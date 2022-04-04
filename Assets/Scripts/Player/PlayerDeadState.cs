using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : IState
{
    private Player player;

    private Rigidbody2D rigidBody;
    private IInputable input;

    private Animator animator;
    public PlayerDeadState(Player player)
    {
        this.player = player;
        rigidBody = this.player.GetComponent<Rigidbody2D>();
        input = this.player.GetComponent<IInputable>();
        animator = this.player.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void Enter() { }
    public void Execute()
    {
        animator.SetBool("isFalling", true);
    }
    public void Exit() { }
    public void OnTriggerEnter2D(Collider2D other) { }
}
