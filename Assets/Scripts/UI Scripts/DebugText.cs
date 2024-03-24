using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI isDashingText;
    [SerializeField] private TextMeshProUGUI dashTimerText;
    [SerializeField] private TextMeshProUGUI dashCooldownText;
    [SerializeField] private TextMeshProUGUI currSpeed;

    // Check for button presses
    public void Update()
    {
        // Toggle the boolean and update the text

        isDashingText.text = "Is Dashing: " + player.GetComponent<NewMovement>().isDashing.ToString();
        dashTimerText.text = "Dash Timer: " + player.GetComponent<NewMovement>().dashTimer.ToString();
        dashCooldownText.text = "Ammo: " + player.GetComponent<NewCombat>().currentAmmo.ToString();
        currSpeed.text = "Current Speed: " + player.GetComponent<NewMovement>()._currentVelocity.ToString();

        if (player.GetComponent<NewMovement>().dashCooldownTimer <= 0)
        {
            textMesh.text = "Dash: Ready";
        }
        else
        {
            textMesh.text = "Dash: Not Ready";
        }


    }
}
