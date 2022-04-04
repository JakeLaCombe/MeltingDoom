using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeadState : IState
{
    private Player player;

    private Rigidbody2D rigidBody;
    private IInputable input;
    private bool isInLemonateStand;

    private Animator animator;
    public PlayerDeadState(Player player)
    {
        this.player = player;
        rigidBody = this.player.GetComponent<Rigidbody2D>();
        input = this.player.GetComponent<IInputable>();
        animator = this.player.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void Enter()
    {
        rigidBody.velocity = new Vector3(0.0f, 0.0f);
    }
    public void Execute()
    {
        animator.SetBool("isFalling", true);

        if (input.Pause())
        {
            SceneManager.LoadScene("Main Game");
        }
    }
    public void Exit() { }
    public void OnTriggerEnter2D(Collider2D other) { }

    public void OnTriggerExit2D(Collider2D other) { }
}
