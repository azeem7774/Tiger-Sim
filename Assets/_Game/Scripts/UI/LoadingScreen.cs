using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//++Future refinement:Async laoding should be incorporated
public class LoadingScreen : MonoBehaviour
{
    
    [SerializeField] private float m_FakeLoadingTime;
    [SerializeField] private Image m_Forground;
    public Action OnLoadingBarComplete;
    private void OnEnable()
    {
        StartCoroutine(LoadingBar());
    }

    IEnumerator LoadingBar()
    {
        float elapsedTime = 0;
        while (elapsedTime < m_FakeLoadingTime)
        {
            m_Forground.fillAmount = Mathf.Lerp(0, 1, elapsedTime /m_FakeLoadingTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnLoadingBarComplete?.Invoke();
    }
}
