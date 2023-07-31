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
            _movement.Move(_moveDirection);

            GameObject player = GameManager.Instance._currentPlayer.gameObject;
            transform.localScale = new Vector3(transform.position.x > player.transform.position.x ? -1f : 1f, 1f);

            _moveTimer -= Time.deltaTime;
            if (_moveTimer <= 0) //If move timer is 0, pick new movement direction
            {
                _moveTimer = _moveTime;
                _moveDirection = new Vector2(Random.Range(1f, -1f), Random.Range(1f, -1f));

            }

            int random = Random.Range(1, 5);
            if (random >= 3)
            {
                //Attack attempt successful
                _attackScript.TryNewAttack(TargetPlayer());
            }
        }

        public override void Die()
        {
            GameManager.Instance.OnEnemyDied.Invoke();
        }

        private Vector2 TargetPlayer()
        {
            return (GameManager.Instance._currentPlayer.transform.position - transform.position).normalized;
        }
    }
}
