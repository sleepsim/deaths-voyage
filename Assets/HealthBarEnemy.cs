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

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = transform.root.GetComponent<Stats>().maxHealth;
        health = transform.root.GetComponentInParent<Stats>().health;
    }

    // Update is called once per frame
    void Update()
    {
        health = transform.root.GetComponentInParent<Stats>().health;
        
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10);
        }

        if(healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }
    }

    void takeDamage(float damage)
    {
        health -= damage;
    }
}
