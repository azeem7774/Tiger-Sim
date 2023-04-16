using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelWin
{

    public void LevelWinStratergy();
}

public class PickableLevelWinScenario : ILevelWin
{

    public void LevelWinStratergy()
    {
        Debug.Log("Level Win by Pickables");
    }
}

public class PreyAllAnimals : ILevelWin
{
    public void LevelWinStratergy()
    {
        Debug.Log("Level Win by Kill all prey");
    }
}