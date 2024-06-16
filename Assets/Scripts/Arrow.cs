using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;
    public float Force
    {
        get { return force; }
        set { force = value; }
    }
    [SerializeField] private int lifeTime;
    [SerializeField] private TriggerDamage triggerDamage;
    [SerializeField] private Player player;

    public void SetImpuls(Vector2 direction, float force, int bonusDamage, Player player)
    {
        this.player = player;
        triggerDamage.Init(this); // Инициализация собственного уничтожителя стрел
        triggerDamage.Parent = player.gameObject; // назначение родительского объекта
        triggerDamage.Damage += bonusDamage;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        // Повернем стрелу если ускорение отрецательное (влево)
        if (force < 0) { transform.rotation = Quaternion.Euler(0, 180, 0); }
        StartCoroutine(StartLife());
    }

    private IEnumerator StartLife() // Время перед исчезновением стрелы
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
        yield break;
    }

    // Переопределенный метод для уничтожения
    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowToPool(this);
    }
}
