using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.GameManagement;

namespace SwapGame.Inputs
{
    //Placeholder functionality
    //For now the AI will move in random directions based on moveTime
    public class AIInput : InputBase
    {
        [SerializeField] private float _moveTime;

        private float _moveTimer;

        public override void Step()
        {
            movement.Move(_moveDirection);
            //The intention, when implemented, is AI characters will turn to face the player regardless of how they are moving
            _moveTimer -= Time.deltaTime;
            if (_moveTimer <= 0)
            {
                _moveTimer = _moveTime;
                _moveDirection = new Vector2(Random.Range(1f, -1f), Random.Range(1f, -1f));
            }
        }

        private float PlayerDirection()
        {
            GameManager gameManager = GameManager.Instance;
            GameObject player = gameManager.CurrentPlayer;


            float playerDirection = transform.position.x - player.transform.position.x;

            return playerDirection;
        }

        public override void Die()
        {
            GameManager.Instance.AddScore();
            //Increase the player's score
        }
    }
}
