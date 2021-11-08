using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SwapGame.GameManagement;
using SwapGame.CharacterComponents;

namespace SwapGame.Inputs
{
    //This allows the player to control the character directly
    public class PlayerInput : InputBase, InputActionMaps.IPlayerActions
    {
        [SerializeField] private float _swapRange;
        [SerializeField] private LayerMask _swapLayer;
        [SerializeField] private Attack _attackScript;

        private Vector2 _moveVector;
        private Vector3 _aimDirection;
        private bool _attacking;

        public InputActionMaps _controls;


        private void Awake()
        {
            _attackScript.enabled = true;

            //Began implementing the newer Unity input package
            //The input package is horrid and evil but it makes setting up multiple input devices easier in the long-term
            _controls = new InputActionMaps();
            _controls.Player.SetCallbacks(this);

            #region Left stick/WASD
            _controls.Player.Move.performed += ctx => _moveVector = ctx.ReadValue<Vector2>();
            _controls.Player.Move.canceled += ctx => _moveVector = Vector2.zero;
            #endregion

            #region Right stick/Mouse
            //Setting a default aim direction on Awake()
            _aimDirection = new Vector3(0, 1, 0);
            _controls.Player.Aim.performed += ctx => _aimDirection = ctx.ReadValue<Vector2>();
            _controls.Player.MouseAim.performed += ctx =>
            {
                _aimDirection = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
                _aimDirection.z = 0;
                _aimDirection = (_aimDirection - transform.position).normalized;
            };
            #endregion

            #region Attacking
            _controls.Player.Fire.performed += ctx => _attacking = true;
            _controls.Player.Fire.canceled += ctx => _attacking = false;
            #endregion

            #region Swapping
            _controls.Player.Swap.performed += ctx =>
            {
            //This checks all the objects along the raycast, then checks which one is closer
            RaycastHit2D[] swapCheck = Physics2D.RaycastAll(transform.position, _aimDirection, _swapRange, _swapLayer);

                GameObject closestObj = null;

                Vector3 pos = gameObject.transform.position;

                foreach (var obj in swapCheck)
                {
                    GameObject foundObject = obj.collider.gameObject;

                    if (foundObject != gameObject)
                    {
                        if (closestObj == null || obj.distance < Vector2.Distance(closestObj.transform.position, pos))
                        {
                        //closestObj stores whichever object is closest as of the current iteration
                        closestObj = foundObject;
                        }
                    }
                }
            //Have to check closestObj has been assigned
            //Otherwise, if the raycast doesn't hit anything, it returns an error as it tries to print the name of a non-existant object
            if (closestObj != null)
                {
                    var manager = GameManager.Instance;
                    manager.SetNewPlayer(closestObj);
                    print("Swapped to " + closestObj.name);
                }
            };
            #endregion
        }

        public override void Step()
        {
            movement.Move(_moveVector);
            if (_attacking)
            {
                AttemptAttack();
            }
        }

        public override void AttemptAttack()
        {
            if (_attackScript.AttackReady())
            {
                _attackScript.FireProjectile(_aimDirection);
            }
        }

        public override void Die()
        {
            GameManager.Instance.EndGame();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _aimDirection * _swapRange);
        }

        #region Will get errors if these methods are taken out, just leave them in so the input package cooperates
        //Input package now requires these methods to be present in a script using it?
        //Can still do input package things in Awake()
        //Seems like these methods will continue firing as long as the control is being performed?
        public void OnMove(InputAction.CallbackContext context)
        {

        }

        public void OnAim(InputAction.CallbackContext context)
        {

        }

        public void OnFire(InputAction.CallbackContext context)
        {

        }

        public void OnSwap(InputAction.CallbackContext context)
        {

        }

        public void OnMouseAim(InputAction.CallbackContext context)
        {

        }
        #endregion

        void OnEnable()
        {
            _controls.Player.Enable();
        }

        void OnDisable()
        {
            _controls.Player.Disable();
        }
    }
}
