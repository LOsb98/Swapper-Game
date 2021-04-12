using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This allows the player to control the character directly
public class PlayerInput : InputBase
{
    public override void Step()
    {
        print("Using player input");
        //GetAxisRaw() is best for keyboard, will always return integer values for X and Y between -1 and 1
        //GetAxis() adds a smoothing effect to the inputs which can feel like input delay on keyboard
        Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement.Move(moveVector);
    }

    public override void Die()
    {
        //End the game
    }
}
