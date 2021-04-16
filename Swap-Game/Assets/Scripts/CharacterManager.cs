using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Character charData;
    private InputBase currentInput;
    private InputBase playerInput;
    private InputBase aiInput;
    private GameManager gameManager;
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

    void Awake()
    {
        //Sending character data to the relevant components here to avoid passing it to a bunch of component references/method parameters later
        //For attacks, things like fire rate and projectiles can be passed into the Attack script beforehand
        //The input scripts can then call a method to attempt an attack when necessary, all other logic is handled in the Attack script separately
        //May need to pass in an "aim direction" Vector2
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerInput = GetComponent<PlayerInput>();
        aiInput = GetComponent<AIInput>();
        GetComponent<SpriteRenderer>().sprite = charData.sprite;
        GetComponent<Movement>().speed = charData.speed;
        SetPlayer();
    }

    void Update()
    {
        currentInput.Step();
    }

    //Keeping this as a separate method that can be called from other scripts later
    private void SetPlayer()
    {
        if (IsPlayer)
        {
            currentInput = playerInput;
            gameManager.currentPlayer = gameObject;
            playerInput.enabled = true;
            aiInput.enabled = false;
        }
        else 
        {
            currentInput = aiInput;
            playerInput.enabled = false;
            aiInput.enabled = true;
        } 
    }
}
