using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LeveData[] m_Levels;
    [SerializeField] private LevelWin m_LevelWinObject;
    private int m_CurrentLevelIndex;
    private ILevelWin m_LevelWin;
    private LevelTypes m_LevelType;

    #region Properties
    public int CurrentLevel => m_CurrentLevelIndex;
    public LevelTypes LevelType => m_Levels[CurrentLevel].LevelType;
    #endregion

    private void OnEnable()
    {
        PickupManager.OnPickableToZero += LevelWin;
        PreyManager.OnPreyCountZero += LevelWin;
    }

    private void Awake()
    {
        m_CurrentLevelIndex = PreferenceManager.LevelIndex;
    }
    private void Start()
    {
        InitialLevel();
    }

    private void InitialLevel()
    {
        Instantiate(m_Levels[m_CurrentLevelIndex], transform);
        m_Levels[m_CurrentLevelIndex].InitializeLevel();
        m_LevelType = m_Levels[CurrentLevel].LevelType;
        m_LevelWin = WinLevelConditions();
    }

    private ILevelWin WinLevelConditions()
    {
        switch (m_LevelType)
        {
            case LevelTypes.CollectingPickupsLevel:
                return new PickableLevelWinScenario();
            case LevelTypes.PreyLevel:
                return new PreyAllAnimals();
        }
        return null;
    }

    private void LevelWin()
    {
        m_LevelWinObject.ActiveGameObject();
        int levelIndex = PreferenceManager.LevelIndex;
        int unlockLevels = PreferenceManager.UnlockLevel;
        m_LevelWin = WinLevelConditions();
        m_LevelWin.LevelWinStratergy();
        //Debug.LogError($"Unlock = {(levelIndex == unlockLevels - 1)}");
        if (levelIndex == unlockLevels - 1)
        {
            unlockLevels++;
            PreferenceManager.UnlockLevel = unlockLevels;
        }

        if (levelIndex < m_Levels.Length)
        {
            levelIndex++;
        }
        PreferenceManager.LevelIndex = levelIndex;
    }

    private void OnDisable()
    {
        PickupManager.OnPickableToZero -= LevelWin;
        PreyManager.OnPreyCountZero -= LevelWin;
    }
}
