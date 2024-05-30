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
    [SerializeField] private Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            Health health = other.gameObject.GetComponent<Health>();
            health.SetHealth(bonusHealth);
            animator.SetTrigger("StartDestroy"); // Начинаем анимацию удаления аптечки
        }
    }

    public void EndDestroy() // Удаление аптечки со сцены.
    {
        Destroy(this.gameObject);
    }
}
