using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTimer : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float minValue; // Minimum value to correspond to alpha 0
    public float maxValue; // Maximum value to correspond to alpha 1

    // Update is called once per frame
    void Start()
    {
        maxValue = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCombat>().timeBetweenAttacks;
    }
    void Update()
    {
        // Example: Get the value from some game variable
        float value = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCombat>().timeUntilNextAttack;

        float range = maxValue - minValue;
        // Map the value to the alpha range
        float normalizedValue = 1f - Mathf.Clamp01((value - minValue) / range) * 0.8f + 0.2f;

        // Set the alpha value of the canvas group
        canvasGroup.alpha = normalizedValue;

        // Debug.Log(normalizedValue);
    }

    float GetGameValue()
    {
        // Example method to get the game value you want to use
        // Replace this with your own method to retrieve the desired value
        return Mathf.Sin(Time.time); // Example: Using sine function for demonstration
    }
}
