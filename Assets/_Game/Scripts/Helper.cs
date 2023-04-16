using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static void ToggleGameObjects(GameObject[] gos, bool enable)
    {
        for (int i = 0; i < gos.Length; i++)
            gos[i].SetActive(enable);
    }

    
}
