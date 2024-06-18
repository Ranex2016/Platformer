using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Health health;
    private float direction;
    public float Direction { get { return direction; } }

    private void OnCollisionStay2D(Collision2D other)
    {
        //health = other.gameObject.GetComponent<Health>();
        // Проверим есть ли у столкнувшегося объекта компонент Health
        if(GameManager.Instance.healthContainer.ContainsKey(other.gameObject))
        {
            health = GameManager.Instance.healthContainer[other.gameObject];
        }

        if (health != null)
        {
            direction = (other.transform.position - transform.position).x;
            animator.SetFloat("Direction", Mathf.Abs(direction));
        }
    }

    public void SetDamage()
    {
        if (health != null) { health.TakeHit(damage, gameObject); }
        health = null;
        direction = 0f;
        animator.SetFloat("Direction", direction);
    }
}
