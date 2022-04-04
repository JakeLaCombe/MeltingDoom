using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : IState
{
    private Player player;

    private Rigidbody2D rigidBody;
    private IInputable input;
    private bool isInLemonateStand;
    private Animator animator;

    public PlayerMovingState(Player player)
    {
        this.player = player;
        rigidBody = this.player.GetComponent<Rigidbody2D>();
        input = this.player.GetComponent<IInputable>();
        animator = this.player.GetComponent<Animator>();
        isInLemonateStand = false;
    }
    // Start is called before the first frame update
    public void Enter() { }
    public void Execute()
    {
        if (input.Pause())
        {
            Time.timeScale = Time.timeScale == 0.0f ? 1.0f : 0.0f;
        }

        if (Time.timeScale <= 0.0f)
        {
            animator.speed = 0.0f;
            return;
        }

        animator.speed = 1.0f;

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

        if (input.ActionHold() && Mathf.Abs(vy) <= 0.001f)
        {
            vy = 6.0f;
            // SoundManager.instance.Jump.Play();
        }
        else if (!input.ActionHold() && vy > 1.0f)
        {
            vy = 1.0f;
        }

        if (isInLemonateStand && input.SecondaryAction())
        {
            LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

            if (levelManager.GetCoins() >= 5)
            {
                levelManager.RemoveCoins(5);
                levelManager.decreaseTemperature(10);
            }
        }

        rigidBody.velocity = new Vector2(vx, vy);

        Debug.Log(vx + " : " + vy);
        animator.SetBool("isRunning", Mathf.Abs(vx) > 0.0001f);
        animator.SetBool("isJumping", Mathf.Abs(vy) > 0.0001f);
        animator.SetBool("isFalling", vy < -0.0001f);
    }
    public void Exit() { }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lemonade Stand")
        {
            isInLemonateStand = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Lemonade Stand")
        {
            isInLemonateStand = false;
        }
    }
}
