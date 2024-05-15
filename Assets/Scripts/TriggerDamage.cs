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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверка на столкновение с родительским колайдером.
        if (other.gameObject == parent)
        { return; }

        var health = other.gameObject.GetComponent<Health>();
        if (health != null && isDestroyingAfterCollision)
        {
            //Debug.Log(other.gameObject.name + " напал на "+ parent.name);
            health.TakeHit(damage);
            print("Стрела уничтожена!");
            Destroy(this.gameObject);
        }
    }
}
