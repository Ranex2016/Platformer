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
    [SerializeField] private Camera playerCamera;

    private float bonusForce;
    private float bonusDamage;
    private float bonusHealth;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundDetection groundDetection;
    private Vector2 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isJumping;
    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float damageForce;
    private Arrow currentArrow;
    private float cooldown = 0.3f; // время перезарядки
    private bool isCooldown = false;
    private List<Arrow> arrowsPool;
    private int arrowsCount = 5;
    private bool isBlockMovement;
    private Health health; // Нужно для полученичя доступа для шкалы здоровья
    public Health Health { get { return health; } }

    public UICharacterController controller;

    public float Speed // Свойство скорости
    {
        get { return speed; }
        set
        {
            if (value <= 0.5f) { speed = 0.5f; }
            else { speed = value; }
        }
    }

    [SerializeField] private BuffReciever buffReciever;

    private void Start()
    {
        controller = GetComponent<UICharacterController>();
        InitUIController(controller);
        buffReciever = GetComponent<BuffReciever>();
        health = GetComponent<Health>();

        // Заполнение пула стрелами
        arrowsPool = new List<Arrow>();
        for (int i = 0; i < arrowsCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            arrowTemp.gameObject.SetActive(false);
            arrowsPool.Add(arrowTemp);

        }

        buffReciever.OnBuffChanged += ApplyBuffs;
        health.OnTakeHit += TakeHit;
    }

    public void ApplyBuffs()
    {
        Debug.Log("Произошел вызов делегата");
        var forceBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Force);
        var damageBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Damage);
        var armoreBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Armore);

        bonusForce = forceBuff == null ? 0 : forceBuff.additiveBonus;
        bonusDamage = damageBuff == null ? 0 : damageBuff.additiveBonus;
        bonusHealth = armoreBuff == null ? 0 : armoreBuff.additiveBonus;
        health.SetHealth((int)bonusHealth);
    }

    private void TakeHit(int damage, GameObject attacker)
    {
        animator.SetTrigger("GetDamage");
        isBlockMovement = true;
        // Добавляем приложеную силу от урона противника что бы он отскакивал, учитывая с какой стороны урон
        rb.AddForce(transform.position.x < attacker.transform.position.x ?
                                            new Vector2(-damageForce, 3f) :
                                            new Vector2(damage, 3f), ForceMode2D.Impulse);
    }

    // Вызывается перед началом анимации хотьбы
    private void UnblockMovement()
    {
        isBlockMovement = false;
    }

    private void Update()
    {
        // Управляем анимациех ходьбы
        animator.SetFloat("Speed", Mathf.Abs(direction.x));
        Move();
        CheckFall();

        //Проверяем что герой уничтожен
        if (health.CurrentHealth <= 0)
        {
            // Вызовем метод чтобы скинуть камеру с героя в корень сцены.
            OnDestroy();
        }
        //CheckShoot();
    }

    private void Move()
    {
        animator.SetBool("isGrounded", groundDetection.IsGrounded);
        isJumping = isJumping && !groundDetection.IsGrounded;

        if (!isJumping && !groundDetection.IsGrounded) // если мы не пригнулы и не касаемся земли, то это подение
        {
            //animator.SetTrigger("StartFall");
        }
        direction = Vector2.zero; // (0,0)
        if (Input.GetKey(KeyCode.A) || controller.Leaft.IsPressed)
        {
            //transform.Translate(Vector2.left * Time.deltaTime * speed);
            direction = Vector2.left;
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D) || controller.Right.IsPressed)
        {
            //transform.Translate(Vector2.right * Time.deltaTime * speed);
            direction = Vector2.right;
            spriteRenderer.flipX = false;
        }
        direction *= speed;
        direction.y = rb.velocity.y;
        // Двигаемся если движение не заблокировано
        if (!isBlockMovement)
        {
            rb.velocity = direction;
        }


        // отмена стрельбы если персонаж находится не на земле или движется
        if (!groundDetection.IsGrounded || direction.x > 0 || direction.x < 0)
        {
            animator.SetBool("StartAttack", false);
        }
    }

    private void Jump()
    {
        if (groundDetection.IsGrounded) // Прыжок игрока
        {
            Debug.Log("Прыжок!");
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }
    }

    // Создание стрелы
    public void CheckShoot()
    {
        if (groundDetection.IsGrounded && !isCooldown)
        {
            animator.SetBool("StartAttack", true);
        }
    }

    private Arrow GetArrowFromPool()
    {
        if (arrowsPool.Count > 0)
        {
            var arrowTemp = arrowsPool[0];
            arrowsPool.Remove(arrowTemp);
            arrowTemp.transform.parent = null; // извлечение из родительского объекта
            arrowTemp.transform.position = arrowSpawnPoint.position; // присвоение позиции спавна
            arrowTemp.gameObject.SetActive(true);
            return arrowTemp;
        }
        return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
    }

    // Возврат стрелы в пул
    public void ReturnArrowToPool(Arrow arrowTemp)
    {
        if (!arrowsPool.Contains(arrowTemp))
        {
            arrowsPool.Add(arrowTemp);
        }
        arrowTemp.transform.parent = arrowSpawnPoint; // 
        arrowTemp.transform.position = arrowSpawnPoint.position; // 
        arrowTemp.gameObject.SetActive(false);
    }

    public void StartAttack() // Метод запускаемый из анимации Archery
    {
        //currentArrow = Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
        currentArrow = GetArrowFromPool();
        currentArrow.SetImpuls(
                                Vector2.right,
                                (spriteRenderer.flipX ? -force * shootForce : force * shootForce),
                                (int)bonusDamage,
                                this);
        animator.SetBool("StartAttack", false);

        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
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
        if (GameManager.Instance.coinContainer.ContainsKey(other.gameObject))
        {
            PlayerInventory.Instance.CoinsCount += 1; // Добавляем манетки
            print("Манет у игрока: " + PlayerInventory.Instance.CoinsCount);

            // Уничтожить манетку со сцены
            var coin = GameManager.Instance.coinContainer[other.gameObject];
            coin.StartDestroy();
            PlayerInventory.Instance.CoinsText.text = PlayerInventory.Instance.CoinsCount.ToString();
            //Destroy(other.gameObject);
        }

        if (GameManager.Instance.itemConteiner.ContainsKey(other.gameObject))
        {
            var itemComponent = GameManager.Instance.itemConteiner[other.gameObject];
            PlayerInventory.Instance.items.Add(itemComponent.Item);
            itemComponent.Destroy(other.gameObject);
        }
    }

    // Инициализация кнопок упрваления
    public void InitUIController(UICharacterController uiController)
    {
        controller = uiController;
        // Стандартная подписка на нажатие кнопки, передаем ссылку на метод прыжка
        controller.Jump.onClick.AddListener(Jump);
        // Стандартная подписка на нажатие кнопки, передаем ссылку на метод стрельбы
        controller.Fire.onClick.AddListener(CheckShoot);
    }

    // должен вызываться при уничтожении нашего игрока
    public void OnDestroy()
    {
        playerCamera.transform.parent = null;
        playerCamera.enabled = true;
    }
}
