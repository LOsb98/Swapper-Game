using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This allows the player to control the character directly
public class PlayerInput : InputBase, InputActionMaps.IPlayerActions
{
    public InputActionMaps controls;
    private Vector2 moveVector;
    private Vector2 aimDir;

    void Awake()
    {
        //Began implementing the newer Unity input package
        //The input package is horrid and evil but it makes setting up multiple input devices easier in the long-term
        controls = new InputActionMaps();
        controls.Player.SetCallbacks(this);
        //Left stick/WASD
        controls.Player.Move.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveVector = Vector2.zero;
        //Right stick/Mouse
        controls.Player.Aim.performed += ctx => aimDir = ctx.ReadValue<Vector2>();
    }

    public override void Step()
    {
        movement.Move(moveVector);
    }

    public override void Die()
    {
        //End the game
    }

    #region Will get errors if these methods are taken out, just leave them in so the input package cooperates
    //Input package now requires these methods to be present in a script using it?
    //Can still do input package things in Awake()
    //Also can't cancel an input with these methods (i.e. Move.canceled making the character stop when the stick is at the neutral position)
    public void OnMove(InputAction.CallbackContext context)
    {

    }

    public void OnAim(InputAction.CallbackContext context)
    {

    }

    public void OnFire(InputAction.CallbackContext context)
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
