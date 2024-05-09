
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

    private void Start()
    {
        groundDetection = GetComponent<GroundDetection>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isRightDerection == true && groundDetection.getIsGrounded() == true)
        {
            rigidBody.velocity = Vector2.right * speed;
            if (transform.position.x >= rightBorder.transform.position.x)
            {
                isRightDerection = !isRightDerection;
            }
        }
        else if (groundDetection.getIsGrounded() == true)
        {
            rigidBody.velocity = Vector2.left * speed;
            if (transform.position.x <= leftBorder.transform.position.x)
            {
                isRightDerection = !isRightDerection;
            }
        }

        // поворачиваем спрайт по х в зависимости от направления движения
        if (rigidBody.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
            if (groundDetection.getIsGrounded())
            {
                animator.SetFloat("Speed", rigidBody.velocity.x);
            }else{
                animator.SetFloat("Speed", 0);
            }
            
        }
        else
        {
            spriteRenderer.flipX = false;
            if(groundDetection.getIsGrounded())
            {
                animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
            }
            else{
                animator.SetFloat("Speed", 0);
            }
            
        }
    }
}
