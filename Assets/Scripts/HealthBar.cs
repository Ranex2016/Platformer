using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    private float healthValue;
    private float currentHealth;
    private Player player;
    [SerializeField] private float delta; // шажок на который мы подходим


    void Start()
    {
        player = FindObjectOfType<Player>();
        healthValue = player.Health.CurrentHealth / 100.0f;
    }

    void Update()
    {
        currentHealth = player.Health.CurrentHealth / 100.0f;
        if (currentHealth > healthValue)
        {
            healthValue += delta;
        }
        if (currentHealth < healthValue)
        {
            healthValue -= delta;
        }
        if (Mathf.Abs(currentHealth - healthValue) < delta)
        {
            healthValue = currentHealth;
        }
        health.fillAmount = healthValue;
    }
}
