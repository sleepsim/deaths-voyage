using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMaze : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject fogFinal;

    void Update()
    {
        if (enemy1 == null && enemy2 == null)
        {
            fogFinal.SetActive(true);
        }
    }
}
