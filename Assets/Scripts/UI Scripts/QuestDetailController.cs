using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDetailController : MonoBehaviour
{
    [SerializeField] GameObject parentCanvas;
    [SerializeField] Button acceptButton;
    TextMeshProUGUI info;
    TextMeshProUGUI title;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
    }

    public void clickAccept()
    {
        parentCanvas.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
