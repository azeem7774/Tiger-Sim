using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeveData : MonoBehaviour
{
    
    [SerializeField] private LevelTypes m_LevelType;
    [SerializeField] private int m_LevelIndex;
    [SerializeField] private string m_LevelName;
    [SerializeField] private Transform m_PlayerSpawnPos;
    [SerializeField] private string m_LevelObjective;
    [SerializeField] private string[] m_LevelInstructions;
    private PickupManager m_PickupManager;
    
    public LevelTypes LevelType => m_LevelType;
    // I need to find the best solution to get player position
    
    public void InitializeLevel()
    {
        SetPlayerPosition();
        
    }


    private void SetPlayerPosition()
    {
        Transform player = PlayerData.Instance.gameObject.transform;
        player.position = m_PlayerSpawnPos.position;
    }

    
}

public enum LevelTypes
{
    CollectingPickupsLevel,
    PreyLevel,
    FindingThingsLevel,
    RaceLevel,
    KillEnemiesLevel,
    CompleteTaskLevel,
    Mix,
}
