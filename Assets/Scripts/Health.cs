using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    public int CurrentHealth { get { return health; } }
    [SerializeField] private int maxHealth = 100;

    private void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }
    /// <summary>
    /// Метод нанесения урона
    /// </summary>
    /// <param name="damage"></param>
    public void TakeHit(int damage)
    {
        // Проверим что урон не больше чем здоровья у игрока, иначе их сравняем
        if (health < damage)
        {
            damage = health;
        }
        health -= damage;
        if (health <= 0) { Destroy(gameObject); }
    }

    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
