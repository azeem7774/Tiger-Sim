using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour, ICountableThings
{
    // this action should not be static
    public static Action OnPickableToZero;
    [SerializeField] private Pickable[] m_Pickables;
    private int m_PickableCount;

    private void OnEnable()
    {
        m_PickableCount = m_Pickables.Length;
        SubscribePickable();
    }

    private void SubscribePickable()
    {
        for (int i = 0; i < m_PickableCount; i++)
             m_Pickables[i].OnPickupCollect += DecrementCount;
    }

    public void DecrementCount()
    {
        m_PickableCount--;
        if (m_PickableCount <= 0)
        {
            OnPickableToZero?.Invoke();
        }
    }
}
