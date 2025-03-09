using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateData", menuName = "Game/GameStateData")]
public class GameStateData : ScriptableObject
{
    public bool doneClickingBlock1;
    public bool doneClickingBlock2;
    public bool doneClickingBlock3;
}
