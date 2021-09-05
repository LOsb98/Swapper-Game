using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeholder functionality
//For now the AI will move in random directions based on moveTime
public class AIInput : InputBase
{
    [SerializeField]
    private Vector2 moveDir;
    private float moveTimer;
    public float moveTime;

    public override void Step()
    {
        movement.Move(moveDir);
        //The intention, when implemented, is AI characters will turn to face the player regardless of how they are moving
        if (moveDir.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDir.x > 0) transform.localScale = new Vector3(1, 1, 1);
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            moveTimer = moveTime;
            moveDir = new Vector2(Random.Range(1f, -1f), Random.Range(1f, -1f));
        }

    }

    public override void Die()
    {
        GameManager.Instance.AddScore(0);
        //Increase the player's score
    }
}
