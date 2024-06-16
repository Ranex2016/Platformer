using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private ItemType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Item item; // экземпляр предмета соответствующий типу ItemType
    public Item Item
    {
        get { return item; }
        //set{item = value;}
    }
    private Animator animator;

    public void Destroy(GameObject gameObject)
    {
        animator.SetTrigger("StartDestroy");
    }

    public void EndDestroy()
    {
        MonoBehaviour.Destroy(this.gameObject);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        item = GameManager.Instance.itemDataBase.GetItemOfID((int)type);
        spriteRenderer.sprite = item.Icon; // назначаем иконку зелья
        GameManager.Instance.itemConteiner.Add(gameObject, this); // Добавляем сами себя(игровой объект к которому крепится скрипт) в словарик.
    }
}

public enum ItemType
{
    DamagePotion = 1,
    ArmorPotion = 2,
    ForcePotion = 3
}