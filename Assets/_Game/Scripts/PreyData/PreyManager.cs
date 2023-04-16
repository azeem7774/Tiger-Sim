using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyManager : MonoBehaviour, ICountableThings
{
    public static Action OnPreyCountZero;
    [SerializeField] private Preyable[] m_Preyables;
    public int CountLength => m_Preyables.Length;
    private int m_PreyCounter;
    private void OnEnable()
    {
        m_PreyCounter = CountLength;
        SubscribePrey();
    }
    public void DecrementCount()
    {
        m_PreyCounter--;
//        Debug.Log(m_PreyCounter);
        if (m_PreyCounter <= 0)
        {
            OnPreyCountZero?.Invoke();
//            Debug.Log("Prey counter zero...");
            
        }
    }

    private void SubscribePrey()
    {
       for (int i = 0; i < CountLength; i++)
            m_Preyables[i].OnPreyDeath += DecrementCount;
    }
}
