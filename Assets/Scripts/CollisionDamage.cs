using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private string collisionTag;
    [SerializeField] private Animator animator;
    private float timeBetweenAttacks = -1;
    private void OnCollisionStay2D(Collision2D other)
    {
        // проверяем столкновение объектов по тегу
        if (other.gameObject.CompareTag(collisionTag))
        {
            Health health = other.gameObject.GetComponent<Health>();


            if (timeBetweenAttacks < Time.time)
            {
                timeBetweenAttacks = Time.time + 1;
                if (this.gameObject.tag == "Enemy")
                {
                    animator.SetTrigger("findPlayer");
                }
                health.TakeHit(damage);
            }

        }
    }

}
