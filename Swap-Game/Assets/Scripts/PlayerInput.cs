using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This allows the player to control the character directly
public class PlayerInput : InputBase, InputActionMaps.IPlayerActions
{
    [SerializeField] private float _swapRange;

    public InputActionMaps controls;
    private Vector2 moveVector;
    private Vector3 aimDir;
    public LayerMask swapLayer;

    private void Awake()
    {
        //Began implementing the newer Unity input package
        //The input package is horrid and evil but it makes setting up multiple input devices easier in the long-term
        controls = new InputActionMaps();
        controls.Player.SetCallbacks(this);

        #region Left stick/WASD
        controls.Player.Move.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveVector = Vector2.zero;
        #endregion

        #region Right stick/Mouse
        //Setting a default aim direction on Awake()
        aimDir = new Vector3(0, 1, 0);
        controls.Player.Aim.performed += ctx => aimDir = ctx.ReadValue<Vector2>();
        controls.Player.MouseAim.performed += ctx =>
        {
            aimDir = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            aimDir.z = 0;
            aimDir = (aimDir - transform.position).normalized;
        };
        #endregion

        #region Attacking
        controls.Player.Fire.performed += ctx => print("Fired");
        #endregion

        #region Swapping
        controls.Player.Swap.performed += ctx =>
        {
            //This checks all the objects along the raycast, then checks which one is closer
            RaycastHit2D[] swapCheck = Physics2D.RaycastAll(transform.position, aimDir, _swapRange, swapLayer);

            GameObject closestObj = null;

            foreach(var obj in swapCheck)
            {
                if(obj.collider.gameObject.name != gameObject.name)
                {
                    if(closestObj == null || obj.distance < Vector2.Distance(closestObj.transform.position, gameObject.transform.position))
                    {
                        //closestObj stores whichever object is closest as of the current iteration
                        closestObj = obj.collider.gameObject;
                    }
                }
            }
            //Have to check closestObj has been assigned
            //Otherwise, if the raycast doesn't hit anything, it returns an error as it tries to print the name of a non-existant object
            if (closestObj != null)
            {
                GameManager.Instance.SetNewPlayer(closestObj);
                print("Swapped to " + closestObj.name);
            } 
        };
        #endregion
    }

    public override void Step()
    {
        movement.Move(moveVector);
    }

    public override void Die()
    {
        GameManager.Instance.EndGame();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, aimDir * _swapRange);
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
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }
}
