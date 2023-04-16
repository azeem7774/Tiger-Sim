using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action OnGameWin;
    [SerializeField] private LevelManager m_LevelManager;

    private LevelTypes m_LevelType;

    private void Start()
    {
        m_LevelType = m_LevelManager.LevelType;
    }

}
