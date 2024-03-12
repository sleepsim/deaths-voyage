using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOnTrigger : MonoBehaviour
{
    [SerializeField] string SceneOther;
    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneOther);
    }
}
