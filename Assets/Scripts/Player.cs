using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.VersionControl;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value <= 0.5f)
            { speed = 0.5f; }
            else
            {
                speed = value;
            }
        }
    }
    [SerializeField] private float force;
    [SerializeField] private float minHeight = -10;
    [SerializeField] private bool isCheatMode = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundDetection groundDetection;
    private Vector2 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isJumping;

    private void Start()
    {
        // Animal dog = new Dog();
        // dog.Message();
        // dog.Sound();
        // Animal cat = new Cat();
        // cat.Message();
        // cat.Sound();
        // Vehicle car = new Car();
        // car.Bip();
        // Vehicle truck = new Truck();
        // truck.Bip();
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

abstract class Animal : IAnimal
{
    public string name;
    public int ageOfDays;
    public abstract void Message();
    public abstract void Sound();
}

class Dog : Animal
{
    public override void Message()
    {
        Debug.Log("Гав");
    }
    public override void Sound()
    {
        Debug.Log("Я собака!");
    }
}

class Cat : Animal
{
    public override void Message()
    {
        Debug.Log("Мяу");
    }

    public override void Sound()
    {
        Debug.Log("Я кот!");
    }
}

interface IAnimal
{
    void Sound();
}

abstract class Vehicle
{
    public string name;
    public virtual void Bip()
    {
        name = "Машина";
        Debug.Log(name + ": Гудок!");
    }
}

class Car : Vehicle
{
    public override void Bip()
    {
        base.Bip();
        name = "Масквич 2106";
        Debug.Log(name + ": Бибик!");
    }
}

class Truck : Vehicle
{
    public override void Bip()
    {
        base.Bip();
        name = "MAN";
        Debug.Log(name + ": Пау-Пау!");
    }
}
