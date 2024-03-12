using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWin : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject fogFinal;

    void Update()
    {
        if (enemy1 == null)
        {
            fogFinal.SetActive(true);
        }
    }
}
