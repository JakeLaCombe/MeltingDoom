using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : IState
{
    private Player player;

    private Rigidbody2D rigidBody;
    private IInputable input;
    public PlayerMovingState(Player player)
    {
        this.player = player;
        rigidBody = this.player.GetComponent<Rigidbody2D>();
        input = this.player.GetComponent<IInputable>();
    }
    // Start is called before the first frame update
    public void Enter() { }
    public void Execute()
    {

        float vx = rigidBody.velocity.x;
        float vy = rigidBody.velocity.y;

        if (input.LeftHold())
        {
            vx = -5.0f;
            player.gameObject.transform.localScale = new Vector2(-1.0f, 1.0f);
        }
        else if (input.RightHold())
        {
            vx = 5.0f;
            player.gameObject.transform.localScale = new Vector2(1.0f, 1.0f);
        }
        else
        {
            vx = 0.0f;
        }

        if (input.ActionHold() && Mathf.Abs(vy) <= 0.0001f)
        {
            vy = 11.0f;
            // SoundManager.instance.Jump.Play();
        }
        else if (!input.ActionHold() && vy > 1.0f)
        {
            vy = 1.0f;
        }

        rigidBody.velocity = new Vector2(vx, vy);
        // player.animator.SetBool("isRunning", Mathf.Abs(vx) > 0.0001f);
        // player.animator.SetBool("isAirborne", Mathf.Abs(vy) > 0.0001f);

        if (!player.GetComponent<SpriteRenderer>().isVisible && player.transform.position.y < -15.0f)
        {
            GameObject.Destroy(player.gameObject);
        }
    }
    public void Exit() { }
    public void OnTriggerEnter2D(Collider2D other) { }
}
