using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Character charData;
    private InputBase input;
    private bool isPlayer;
    public bool IsPlayer
    {
        get { return isPlayer; }
        set
        {
            isPlayer = value;
            SetPlayer();
        }
    }

    void Start()
    {
        //Sending character data to the relevant components here to avoid passing it to a bunch of component references/method parameters later
        //For attacks, things like fire rate and projectiles can be passed into the Attack script beforehand
        //The input scripts can then call a method to attempt an attack when necessary, all other logic is handled in the Attack script separately
        //May need to pass in an "aim direction" Vector2
        IsPlayer = true;
        GetComponent<SpriteRenderer>().sprite = charData.sprite;
        GetComponent<Movement>().speed = charData.speed;
        SetPlayer();
    }

    void Update()
    {
        input.Step();
    }

    //Keeping this as a separate method that can be called from other scripts later
    private void SetPlayer()
    {
        if (IsPlayer) input = GetComponent<PlayerInput>();
        else input = GetComponent<AIInput>();
    }
}
