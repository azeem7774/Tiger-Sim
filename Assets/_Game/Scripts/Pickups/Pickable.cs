using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, IInteractable
{
    private GameObject destroyFx;
    public Action OnPickupCollect;

    public void Interact()
    {
        //Debug.Log($"{gameObject.name} is collected");
        destroyFx = Resources.Load("FX/BlastFx") as GameObject;
        destroyFx = Instantiate(destroyFx, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(destroyFx, 2);
        Destroy(gameObject,1);
        OnPickupCollect?.Invoke();
    }
}
