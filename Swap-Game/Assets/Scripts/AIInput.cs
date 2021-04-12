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
    private float MoveTimer
    {
        get { return moveTimer; }
        set
        {
            moveTimer = value;
            if (moveTimer <= 0)
            {
                moveTimer = moveTime;
                moveDir = new Vector2(Random.Range(1f, -1f), Random.Range(1f, -1f));
            }

        }
    }
    public float moveTime;

    public override void Step()
    {
        print("Using AI input");
        movement.Move(moveDir);
        //The intention, when implemented is AI characters will turn to face the player regardless of how they are moving
        if (moveDir.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDir.x > 0) transform.localScale = new Vector3(1, 1, 1);
        MoveTimer -= Time.deltaTime;

    }

    public override void Die()
    {
        //Increase the player's score
    }
}
