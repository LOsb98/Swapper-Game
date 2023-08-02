using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwapGame.GameManagement;
using SwapGame.StaticMethods;

namespace SwapGame.Inputs
{
    //Placeholder functionality
    //For now the AI will move in random directions based on moveTime
    public class AIInput : InputBase
    {
        [SerializeField] private float _moveTime;
        private float _moveTimer;

        [SerializeField] private float _attackTime = 1f; //Current default value
        private float _attackTimer;

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

            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0) //Do this every second or so, otherwise this would be done every frame and basically means the AI attacks at full speed, which we don't want
            {
                _attackTimer = _attackTime;
                if (MathsStuff.CoinToss())
                {
                    Debug.Log("Attempting AI attack");
                    //Attack attempt successful
                    _attackScript.TryNewAttack(TargetPlayer());
                }
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
