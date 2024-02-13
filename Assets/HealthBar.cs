using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth;
    public float health;
    private float lerpSpeed = 0.02f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = player.GetComponent<Stats>().maxHealth;
        health = player.GetComponent<Stats>().health;

        Debug.Log(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        health = player.GetComponent<Stats>().health;

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
        player.GetComponent<Stats>().health -= damage;
    }
}
