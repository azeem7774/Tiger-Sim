using System;
using System.Collections;
using System.Collections.Generic;
using MalbersAnimations.Controller;
using UnityEngine;

public class Preyable : MonoBehaviour
{
    [SerializeField] Collider[] m_Colliders;
    public Action OnPreyDeath;
    public void Die()
    {
        DisableColliders();
        OnPreyDeath?.Invoke();
        
    }

    void DisableColliders()
    {
        foreach (var collider in m_Colliders)
            collider.enabled = false;
    }

    public void EmmitDust()
    {
        GameObject dustParticle = Instantiate(Resources.Load("FX/DustRing"), transform.position, Quaternion.identity ) as GameObject;
        Destroy(dustParticle, 10);
    }
}
