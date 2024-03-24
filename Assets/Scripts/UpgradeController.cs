using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] GameObject parentCanvas;
    [SerializeField] private Button op1;
    [SerializeField] private Button op2;
    [SerializeField] private Button op3;


    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Update()
    {
        TextMeshProUGUI op1Text = op1.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI op2Text = op2.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI op3Text = op3.GetComponentInChildren<TextMeshProUGUI>();

        op1Text.text = "New Text for Option One";
        op1Text.text = "New Text for Option Two";
        op1Text.text = "New Text for Option Three";
    }
    public void OptionOne()
    {
        parentCanvas.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OptionTwo()
    {
        parentCanvas.SetActive(false);
    }

    public void OptionThree()
    {
        parentCanvas.SetActive(false);
    }

    // On disable/enable
    void OnDisable()
    {
        // Remove Old?
        Debug.Log("PrintOnDisable: script was disabled");
        Time.timeScale = 1;
    }

    void OnEnable()
    {
        // Randomize Options
        Debug.Log("PrintOnEnable: script was enabled");
        Time.timeScale = 0;
    }
}
