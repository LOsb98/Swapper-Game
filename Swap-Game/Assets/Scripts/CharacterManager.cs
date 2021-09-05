using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Character _characterData;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Movement _movement;

    [SerializeField] private InputBase _playerInput;
    [SerializeField] private InputBase _aiInput;

    private InputBase _currentInput;


    void Awake()
    {
        //Sending character data to the relevant components here to avoid passing it to a bunch of component references/method parameters later
        //For attacks, things like fire rate and projectiles can be passed into the Attack script beforehand
        //The input scripts can then call a method to attempt an attack when necessary, all other logic is handled in the Attack script separately
        //May need to pass in an "aim direction" Vector2
        _health = _characterData.health;
        _spriteRenderer.sprite = _characterData.sprite;
        _movement.speed = _characterData.speed;

    }

    void Update()
    {
        _currentInput.Step();
    }

    //Keeping this as a separate method that can be called from other scripts later
    public void SetPlayerControl(bool enabled)
    {
        _playerInput.enabled = enabled;
        //aiInput.enabled = !enabled;
    }
}
