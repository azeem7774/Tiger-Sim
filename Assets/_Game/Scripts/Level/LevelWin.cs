using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWin : MonoBehaviour
{
    public static Action OnLevelWin;

    public void ActiveGameObject()
    {
        OnLevelWin?.Invoke();
        gameObject.SetActive(true);
    }
}
