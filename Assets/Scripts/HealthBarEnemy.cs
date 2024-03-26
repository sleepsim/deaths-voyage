using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth;
    public float health;
    private float lerpSpeed = 0.02f;

    private Camera _cam;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = transform.parent.GetComponent<StatsEnemy>().maxHealth;
        health = transform.parent.GetComponent<StatsEnemy>().health;

        healthSlider.maxValue = maxHealth;
        easeHealthSlider.maxValue = maxHealth;

        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        health = transform.parent.GetComponent<StatsEnemy>().health;

        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }

        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
}
