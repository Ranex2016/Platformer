using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float force;
    [SerializeField] private float minHeight = -10;
    [SerializeField] private bool isCheatMode = false;
    private Rigidbody2D rb;
    [SerializeField] private GroundDetection groundDetection;
    private Vector2 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetBool("isGrounded", groundDetection.getIsGrounded());
        isJumping = isJumping && !groundDetection.getIsGrounded();

        if(!isJumping && !groundDetection.getIsGrounded()) // если мы не пригнулы и не касаемся земли, то это подение
        {
            animator.SetTrigger("StartFall");
        }
        direction = Vector2.zero; // (0,0)
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector2.left * Time.deltaTime * speed);
            direction = Vector2.left;
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector2.right * Time.deltaTime * speed);
            direction = Vector2.right;
            spriteRenderer.flipX = false;
        }
        direction *= speed;
        direction.y =  rb.velocity.y;
        rb.velocity = direction;

        if (Input.GetKeyDown(KeyCode.Space) && groundDetection.getIsGrounded()) // Прыжок игрока
        {
            Debug.Log("Прыжок!");
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }

        // Управляем анимациех ходьбы
        animator.SetFloat("Speed", Mathf.Abs(direction.x));

        CheckFall();

    }

    /// <summary>
    /// Метод проверки падения
    /// </summary>
    private void CheckFall()
    {
        if (transform.position.y < minHeight && isCheatMode)
        {
            rb.velocity = new Vector2(0, 0);
            transform.position = new Vector3(0, 0, 0);
        }
        else if (transform.position.y < minHeight && !isCheatMode)
        {
            Destroy(gameObject);
        }
    }
}
