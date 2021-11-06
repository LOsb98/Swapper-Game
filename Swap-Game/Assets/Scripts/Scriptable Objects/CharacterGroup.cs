using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Character Group")]
public class CharacterGroup : ScriptableObject
{
    public Character[] _list;
}
