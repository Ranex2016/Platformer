using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start() {
        // все монетки с этим скриптом попадут в массив манеток
        GameManager.Instance.coinContainer.Add(gameObject, this); 
    }

    public void StartDestroy() // Старт анимации уничтожения
    {
        animator.SetTrigger("StartDestroy");
    }

    public void EndDestroy() // уничтожение манетки из анимации
    {
        Destroy(gameObject);
    }
}
