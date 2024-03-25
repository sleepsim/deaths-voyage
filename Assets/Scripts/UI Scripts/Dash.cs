using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    public Slider dashSlider;
    public float maxVal;
    public float minVal;
    private float lerpSpeed = 0.02f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get the dashCooldownTimer value from the player's NewMovement component
        // float dashCooldownTimer = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>().dashCooldownTimer;
        // float normalizedValue = Mathf.Clamp01((dashCooldownTimer - minVal) / (maxVal - minVal));
        // // Update the value of the dashSlider directly
        // dashSlider.value = 1f - normalizedValue;

        // // Smooothen
        // dashSlider.value = Mathf.Lerp(dashSlider.value, normalizedValue, lerpSpeed);
    }


}
