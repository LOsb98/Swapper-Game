using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeholder functionality
//For now the AI will move in random directions based on moveTime
public class AIInput : InputBase
{
    [SerializeField] private Vector2 _moveDir;
    [SerializeField] private float _moveTime;

    private float _moveTimer;

    public override void Step()
    {
        movement.Move(_moveDir);
        //The intention, when implemented, is AI characters will turn to face the player regardless of how they are moving
        _moveTimer -= Time.deltaTime;
        if (_moveTimer <= 0)
        {
            _moveTimer = _moveTime;
            _moveDir = new Vector2(Random.Range(1f, -1f), Random.Range(1f, -1f));
        }
    }

    public override void Die()
    {
        GameManager.Instance.AddScore(0);
        //Increase the player's score
    }
}