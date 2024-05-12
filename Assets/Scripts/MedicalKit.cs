using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : MonoBehaviour
{
    [SerializeField] private int bonusHealth = 20;
    public int BonusHealth
    {
        get { return bonusHealth; }
        set
        {
            if (value >= 1) { bonusHealth = value; }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            Health health = other.gameObject.GetComponent<Health>();
            health.SetHealth(bonusHealth);
            Destroy(this.gameObject);
        }
    }
}
