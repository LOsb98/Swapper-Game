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

        private void Start()
        {
            //InvokeRepeating("TryAttack", 2f, 1f);
            StartCoroutine(AttackRoutine());
            StartCoroutine(MoveRoutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public override void Step()
        {
            //movement.Move(_moveDirection);
            //_moveTimer -= Time.deltaTime;

            //GameObject player = GameManager.Instance.CurrentPlayer;

            //transform.localScale = new Vector3(transform.position.x > player.transform.position.x ? -1f : 1f, 1f);

            //if (_moveTimer <= 0)
            //{
            //    _moveTimer = _moveTime;
            //    _moveDirection = new Vector2(Random.Range(1f, -1f), Random.Range(1f, -1f));

            //}
        }

        public override void Die()
        {
            GameManager.Instance.AddScore();
            //Increase the player's score
        }

        private Vector2 TargetPlayer()
        {
            return (GameManager.Instance.CurrentPlayer.transform.position - transform.position).normalized;
        }

        IEnumerator AttackRoutine()
        {
            while (true)
            {
                //Wait for 1 second between attacks
                yield return new WaitForSeconds(1);

                //Check random value to see if we attack this time
                int random = Random.Range(1, 5);

                if (true)
                {
                    //Attack successful
                    _attackScript.TryNewAttack(TargetPlayer());
                }

                yield return null;
            }
        }

        IEnumerator MoveRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_moveTime);

                movement.Move(_moveDirection);

                GameObject player = GameManager.Instance.CurrentPlayer;

                transform.localScale = new Vector3(transform.position.x > player.transform.position.x ? -1f : 1f, 1f);

                _moveDirection = new Vector2(Random.Range(1f, -1f), Random.Range(1f, -1f));

                yield return null;
            }
        }
    }
}
