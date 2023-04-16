using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private GameObject m_UIManager;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        Instantiate(m_UIManager);
        Invoke("ShowBanner", 3);
    }

    void ShowBanner()
    {
        MaxAdManager.Instance.ShowBanner();
    }
}
