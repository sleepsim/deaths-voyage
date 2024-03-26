using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject parentCanvas;
    [SerializeField] private Button op1;
    [SerializeField] private Button op2;
    [SerializeField] private Button op3;
    TextMeshProUGUI op1Text;
    TextMeshProUGUI op2Text;
    TextMeshProUGUI op3Text;
    private string[] options = new string[] { "addProjectile", "increaseDmg", "increaseAmmo" }; // Removed "increaseSpeed"  for now
    private string option1, option2, option3;



    void Start()
    {
        op1Text = op1.GetComponentInChildren<TextMeshProUGUI>();
        op2Text = op2.GetComponentInChildren<TextMeshProUGUI>();
        op3Text = op3.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Update()
    {
        Cursor.visible = true;

        op1Text.text = option1;
        op2Text.text = option2;
        op3Text.text = option3;
    }
    public void OptionOne()
    {
        parentCanvas.SetActive(false);
        gameObject.SetActive(false);
        ActivateUpgrade(option1);
    }

    public void OptionTwo()
    {
        parentCanvas.SetActive(false);
        gameObject.SetActive(false);
        ActivateUpgrade(option2);
    }

    public void OptionThree()
    {
        parentCanvas.SetActive(false);
        gameObject.SetActive(false); ;
        ActivateUpgrade(option3);
    }

    // On disable/enable
    void OnDisable()
    {
        // Remove Old?
        Debug.Log("PrintOnDisable: script was disabled");
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        // Randomize Options
        Debug.Log("PrintOnEnable: script was enabled");

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GenerateChoices();

    }

    private void GenerateChoices()
    {
        option1 = options[UnityEngine.Random.Range(0, options.Length)];

        option2 = options[UnityEngine.Random.Range(0, options.Length)];
        if (option2 == option1)
        {
            option2 = options[UnityEngine.Random.Range(0, options.Length)];
        }

        option3 = options[UnityEngine.Random.Range(0, options.Length)];
        if (option3 == option1 || option3 == option2)
        {
            option3 = options[UnityEngine.Random.Range(0, options.Length)];

        }
    }
    private void ActivateUpgrade(string type)
    {
        if (type == "increaseAmmo")
        {
            player.GetComponent<NewCombat>().maxAmmo += 3;
        }

        if (type == "increaseDmg")
        {
            player.GetComponent<Stats>().damage += 1;
        }

        if (type == "increaseSpeed")
        {
            player.GetComponent<NewMovement>().moveSpeed += 10;
        }

        if (type == "addProjectile")
        {
            player.GetComponent<Stats>().projectileNumber += 1;
            player.GetComponent<NewCombat>().coneAngle += 10;
        }
    }


}
