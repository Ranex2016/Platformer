using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    [SerializeField] private bool isDestroyingAfterCollision;
    private GameObject parent;
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    private IObjectDestroyer destroyer; // Сылка на интерфейс уничтожения объектов

    /// <summary>
    /// Метод для инициализации уничтожителя перед его использованием.
    /// </summary>
    /// <param name="destroyer"></param>
    public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Столкновение - OnTriggerEnter2D");
        // Проверка на столкновение с родительским колайдером.
        if (other.gameObject == parent)
        { return; }

        // проверка в словаре что есть такой объект
        if (GameManager.Instance.healthContainer.ContainsKey(other.gameObject)) 
        {
            // Достаем компонент Health и получаем ссылку на его игровоей объект
            var health = GameManager.Instance.healthContainer[other.gameObject];
            health.TakeHit(damage);
        }

        if (isDestroyingAfterCollision)
        {
            if (destroyer != null)
            {
                print("Стрела отправленна в колчан!");
                destroyer.Destroy(this.gameObject); // уничтожим объект прикрепленный к этому скрипту
            }
            else
            {
                print("Стрела уничтожена!");
                Destroy(this.gameObject);
            }

        }
    }
}

/// <summary>
/// Интерфейс для написания собственного способа уничтожения объектов.
/// Перед использованием проинициализируйте IObjectDestroyer используя метод Init()
/// Иначе уничтожение будет проходить стандартным образом через Destroy()
/// </summary>
public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}
