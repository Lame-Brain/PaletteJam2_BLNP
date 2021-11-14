using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameManager
{
    public static PuzzleManager PUZZLE;
    public static I_am_the_player PLAYER;

    public static Action<Vector2> OnPlayerDeath;
    public static Action<Vector2> OnBlockDeath;
    public static Action<Vector2> OnBombDeath;
}