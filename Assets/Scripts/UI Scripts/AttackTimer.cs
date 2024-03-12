using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackTimer : MonoBehaviour
{
    // public CanvasGroup canvasGroup;
    // public float minValue; // Minimum value to correspond to alpha 0
    // public float maxValue; // Maximum value to correspond to alpha 1
    // public Slider attackSlider;
    // private float lerpSpeed = 0.02f;

    // // Update is called once per frame
    // void Start()
    // {
    //     maxValue = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCombat>().timeBetweenAttacks;
    // }
    // void Update()
    // {
    //     // Example: Get the value from some game variable
    //     float value = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCombat>().timeUntilNextAttack;

    //     float range = maxValue - minValue;
    //     // Map the value to the alpha range
    //     float normalizedValue = 1f - Mathf.Clamp01((value - minValue) / range);

    //     // Set the alpha value of the canvas group
    //     canvasGroup.alpha = normalizedValue;
    //     attackSlider.value = Mathf.Lerp(attackSlider.value, normalizedValue, lerpSpeed);

    //     // Debug.Log(normalizedValue);
    // }

    public CanvasGroup canvasGroup;
    public Slider attackSlider;
    private float lerpSpeed = 0.02f;

    private float maxValue; // Maximum value to correspond to alpha 0
    private float minValue = 0f; // Minimum value to correspond to alpha 1

    void Start()
    {
        maxValue = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCombat>().timeBetweenAttacks;
    }

    void Update()
    {
        float value = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCombat>().timeUntilNextAttack;

        // Ensure minValue and maxValue are correctly set and not changed afterwards
        float range = maxValue - minValue;

        // Ensure the range is positive to avoid division by zero
        if (range > 0f)
        {
            // Map the value to the alpha range
            float normalizedValue = 1f - Mathf.Clamp01((value - minValue) / range);

            // Set the alpha value of the canvas group
            canvasGroup.alpha = normalizedValue;
            attackSlider.value = Mathf.Lerp(attackSlider.value, normalizedValue, lerpSpeed);
        }
        else
        {
            // Handle case where range is zero or negative
            Debug.LogWarning("Invalid range. Unable to normalize value.");
        }

        // Debug.Log(normalizedValue);
    }
}
