using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    private float healthValue;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        //if (player != null)
            //healthValue = player.Health.CurrentHealth;
    }

    void Update()
    {
        health.fillAmount = player.Health.CurrentHealth / 100.0f;
    }
}
