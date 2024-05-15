using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
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

    public void SetImpuls(Vector2 direction, float force, GameObject parant)
    {
        triggerDamage.Parent = parant;
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
}
