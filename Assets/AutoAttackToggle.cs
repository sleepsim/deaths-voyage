using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoAttackToggle : MonoBehaviour
{
    public GameObject player;
    [SerializeField]private TextMeshProUGUI textMesh;

    // Check for button presses
    public void ButtonPress()
    {
        // Toggle the boolean and update the text
        player.GetComponent<RangedCombat>().autoAttackToggle = !player.GetComponent<RangedCombat>().autoAttackToggle;

        if(player.GetComponent<RangedCombat>().autoAttackToggle)
        {
            textMesh.text = "Auto Attack On";
        }
        else
        {
            textMesh.text = "Auto Attack Off";
        }
        
    }
}
