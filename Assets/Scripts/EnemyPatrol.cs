
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    private Rigidbody2D rigidBody;
    [SerializeField] private bool isRightDerection;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private GroundDetection groundDetection;
    [SerializeField] private CollisionDamage collisionDamage;

    private void Start()
    {
        groundDetection = GetComponent<GroundDetection>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (groundDetection.IsGrounded)
        {
            if (transform.position.x > rightBorder.transform.position.x || collisionDamage.Direction < 0)
            { isRightDerection = false; }

            else if (transform.position.x < leftBorder.transform.position.x || collisionDamage.Direction > 0)
            { isRightDerection = true; }

            rigidBody.velocity = isRightDerection ? Vector2.right : Vector2.left;
        }

        // поворачиваем спрайт по х в зависимости от направления движения
        if (rigidBody.velocity.x > 0) { spriteRenderer.flipX = true; }
        if (rigidBody.velocity.x < 0) { spriteRenderer.flipX = false; }
    }
}
