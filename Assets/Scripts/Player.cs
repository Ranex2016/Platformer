using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.VersionControl;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float force;
    [SerializeField] private float shootForce;
    [SerializeField] private float minHeight = -10;
    [SerializeField] private bool isCheatMode = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundDetection groundDetection;
    private Vector2 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isJumping;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    private float cooldown = 0.4f;
    private bool isCooldown = false;

    public float Speed // Свойство скорости
    {
        get { return speed; }
        set
        {
            if (value <= 0.5f) { speed = 0.5f; }
            else { speed = value; }
        }
    }

    void Update()
    {
        animator.SetBool("isGrounded", groundDetection.IsGrounded);
        isJumping = isJumping && !groundDetection.IsGrounded;

        if (!isJumping && !groundDetection.IsGrounded) // если мы не пригнулы и не касаемся земли, то это подение
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
        direction.y = rb.velocity.y;
        rb.velocity = direction;

        if (Input.GetKeyDown(KeyCode.Space) && groundDetection.IsGrounded) // Прыжок игрока
        {
            Debug.Log("Прыжок!");
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }

        // отмена стрельбы если персонаж находится не на земле или движется
        if (!groundDetection.IsGrounded || direction.x > 0 || direction.x < 0)
        {
            animator.SetBool("StartAttack", false);
        }

        // Управляем анимациех ходьбы
        animator.SetFloat("Speed", Mathf.Abs(direction.x));

        CheckFall();
        CheckShoot();
    }

    // Создание стрелы
    public void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0) && groundDetection.IsGrounded && !isCooldown)
        {
            animator.SetBool("StartAttack", true);
        }
    }

    public void StartAttack() // Метод запускаемый из анимации Archery
    {
        GameObject prefab = Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
        prefab.GetComponent<Arrow>().SetImpuls(
                                    Vector2.right,
                                    (spriteRenderer.flipX ? -force * shootForce : force * shootForce),
                                    this.gameObject);
        animator.SetBool("StartAttack", false);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isCooldown = false;
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

    // Проверка столкновения с манеткой
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            PlayerInventory.Instance.CoinsCount += 1;
            Debug.Log("Манет у игрока: " + PlayerInventory.Instance.CoinsCount);
            Destroy(other.gameObject);
        }
    }
}
