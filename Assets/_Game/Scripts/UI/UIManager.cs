using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singloton
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    private int m_SceneIndex;
    private void OnEnable()
    {
        LevelWin.OnLevelWin += TurnOffGameplayUI;
        m_LoadingScreen.OnLoadingBarComplete += LoadScene;
    }

    private void TurnOffGameplayUI()
    {
        Debug.Log("Gameplay UI Off");
        m_LevelWinScreen.SetActive(true);
        m_MobileControls.SetActive(false);
    }

    private void Start()
    {
        SubscribeMainMenuScreenButtons();
        SubscribeLevelScreenButton();
        SubscribeLevelWinButtons();
        SubscribePauseMenuButtons();
        SubscribeMobileControlButtons();
        m_LoadingScreenGameObject.SetActive(true);
    }
    private void LoadScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIndex = sceneIndex % 2 == 0 ? 1 : ++sceneIndex;
        IEnumerator coroutine = LoadSceneWithIndex(sceneIndex);
        StartCoroutine(coroutine);

    }
    IEnumerator LoadSceneWithIndex(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
            yield return null;

        m_LoadingScreenGameObject.SetActive(false);
        TurnOnMainScreen(sceneIndex % 2 != 0);
        //Patch
        if (sceneIndex == 2) TurnOnGamplayUI();


    }

    private void SwitchScreen(GameObject screenOn, GameObject screenOff)
    {
        screenOn.SetActive(true);
        screenOff.SetActive(false);
    }
    private void OnDisable()
    {
        LevelWin.OnLevelWin -= TurnOffGameplayUI;
        m_LoadingScreen.OnLoadingBarComplete -= LoadScene;
        m_LoadingScreen.OnLoadingBarComplete -= TurnOnGamplayUI;
    }

    #region MainMenuScreen
    [Header("MainMenuScreen")][Space(10)]
    [SerializeField] private GameObject m_MainMenuScreen;
    [SerializeField] private Button m_Exit, m_PrivacyPolicy, m_RateUs, m_MoreGames, m_Settings, m_Play;
    private void SubscribeMainMenuScreenButtons()
    {
        m_Exit.onClick.AddListener(OnClickExit);
        m_PrivacyPolicy.onClick.AddListener(OnClickPrivacyPolicy);
        m_RateUs.onClick.AddListener(OnClickRateUs);
        m_MoreGames.onClick.AddListener(OnClickMoreGames);
        m_Settings.onClick.AddListener(OnClickSettings);
        m_Play.onClick.AddListener(OnClickPlay);

    }

    private void OnClickExit()
    {
        Application.Quit();
    }

    public void TurnOnMainScreen(bool activate)
    {
        m_MainMenuScreen.SetActive(activate);
    }
    private void OnClickPlay()
    {
        m_UnlocklLevel = PreferenceManager.UnlockLevel;
        ToggleLevels(m_LevelSelectionButtons, m_LevelSelectionButtons.Length, false);
        ToggleLevels(m_LevelSelectionButtons, m_UnlocklLevel, true);
        m_LevelSelectionScreen.SetActive(true);
        m_MainMenuScreen.SetActive(false);
    }

    private void OnClickSettings()
    {

        throw new NotImplementedException();
    }

    private void OnClickMoreGames()
    {
        throw new NotImplementedException();
    }

    private void OnClickRateUs()
    {
        throw new NotImplementedException();
    }

    private void OnClickPrivacyPolicy()
    {
        Application.OpenURL("https://gamingalestudio.blogspot.com/2023/01/privacy-policy.html");
    }
    #endregion

    #region LevelSelection
    [Header("Level Selection Screen")]
    [Space(10)]
    [SerializeField] private GameObject m_LevelSelectionScreen;
    [SerializeField] private Button[] m_LevelSelectionButtons;
    [SerializeField] private Button m_LevelScreenExitButton;
    private int m_UnlocklLevel;
    

    private void OnCLickLevelSelect(int levelIndex)
    {
        PreferenceManager.LevelIndex = levelIndex;
        m_LoadingScreenGameObject.SetActive(true);
        m_LevelSelectionScreen.SetActive(false);
        MaxAdManager.Instance.ShowMainMenuInterstitial();
    }

    private void SubscribeLevelSelectButton(Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int copy = i;
            buttons[i].onClick.AddListener(() => OnCLickLevelSelect(copy));
        }
    }

    private void SubscribeLevelScreenButton()
    {
        SubscribeLevelSelectButton(m_LevelSelectionButtons);
        m_LevelScreenExitButton.onClick.AddListener(OnClickLevelScreenExit);
    }

    private void OnClickLevelScreenExit()
    {
        SwitchScreen(m_MainMenuScreen, m_LevelSelectionScreen);
        Debug.Log("Press");
    }
    private void ToggleLevels(Button[] buttons, int maxRange, bool enable)
    {
        for (int i = 1; i < maxRange; i++) buttons[i].interactable = enable;
    }

    #endregion

    #region LoadingScreen
    [Header("LoadingScreen")]
    [Space(10)]
    [SerializeField] private GameObject m_LoadingScreenGameObject;
    [SerializeField] private LoadingScreen m_LoadingScreen;

    #endregion

    #region LevelWinScreen
    [Header("LevelWinScreen")]
    [Space(10)]
    [SerializeField] private GameObject m_LevelWinScreen;
    [SerializeField] Button m_NextButton,m_MenuButton;
    void SubscribeLevelWinButtons()
    {
        m_NextButton.onClick.AddListener(OnClickNextButton);
        m_MenuButton.onClick.AddListener(OnClickMenuButton);
    }

    private void OnClickMenuButton()
    {
        m_LevelWinScreen.SetActive(false);
        m_LoadingScreenGameObject.SetActive(true);
    }

    private void OnClickNextButton()
    {
        OnClickMenuButton();
        //m_LevelWinScreen.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region GamplayUIControl
    [Header("GamePlayUI"), Space(10)]
    [SerializeField] private GameObject m_MobileControls;
    [SerializeField] private Button m_PauseButton;
    private void TurnOnGamplayUI()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
            m_MobileControls.SetActive(true);
    }

    private void SubscribeMobileControlButtons()
    {
        m_PauseButton.onClick.AddListener(OnClickPause);
    }

    private void OnClickPause()
    {
        Time.timeScale = 0;
        m_PauseScreen.SetActive(true);
        m_MobileControls.SetActive(false);
    }

    #endregion


    #region PauseScreen

    [Header("PauseScreen")]
    [Space(10)]
    [SerializeField] private GameObject m_PauseScreen;
    [SerializeField] private Button m_ResumeButton, m_RestartButton, m_MainMenuButton;

    private void SubscribePauseMenuButtons()
    {
        m_ResumeButton.onClick.AddListener(OnClickResume);
        m_RestartButton.onClick.AddListener(OnClickRestart);
        m_MainMenuButton.onClick.AddListener(OnClickMainMenu);
    }

    private void OnClickMainMenu()
    {
        m_PauseScreen.SetActive(false);
        LoadScene();
        m_MobileControls.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnClickRestart()
    {
        m_PauseScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        m_MobileControls.SetActive(true);
        Time.timeScale = 1;
    }

    private void OnClickResume()
    {
        m_MobileControls.SetActive(true);
        Time.timeScale = 1;
        m_PauseScreen.SetActive(false);

    }

    #endregion

}
