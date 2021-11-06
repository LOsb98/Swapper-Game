using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This allows the player to control the character directly
public class PlayerInput : InputBase, InputActionMaps.IPlayerActions
{
    [SerializeField] private float _swapRange;
    [SerializeField] private LayerMask _swapLayer;

    private Vector2 _moveVector;
    private Vector3 _aimDir;

    public InputActionMaps _controls;


    private void Awake()
    {
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
        _aimDir = new Vector3(0, 1, 0);
        _controls.Player.Aim.performed += ctx => _aimDir = ctx.ReadValue<Vector2>();
        _controls.Player.MouseAim.performed += ctx =>
        {
            _aimDir = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            _aimDir.z = 0;
            _aimDir = (_aimDir - transform.position).normalized;
        };
        #endregion

        #region Attacking
        _controls.Player.Fire.performed += ctx => print("Fired");
        #endregion

        #region Swapping
        _controls.Player.Swap.performed += ctx =>
        {
            //This checks all the objects along the raycast, then checks which one is closer
            RaycastHit2D[] swapCheck = Physics2D.RaycastAll(transform.position, _aimDir, _swapRange, _swapLayer);

            GameObject closestObj = null;

            Vector3 pos = gameObject.transform.position;

            foreach (var obj in swapCheck)
            {
                GameObject foundObject = obj.collider.gameObject;

                if (foundObject != gameObject)
                {
                    if(closestObj == null || obj.distance < Vector2.Distance(closestObj.transform.position, pos))
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
    }

    public override void Die()
    {
        GameManager.Instance.EndGame();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _aimDir * _swapRange);
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
