using System;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Action OnUpdateCell;
    [SerializeField] private Image icon;
    private Item item;

    private void Awake()
    {
        icon.sprite = null; //Обнулим картинку зелья
    }
    void Start()
    {
        //Инициализация
        //icon = GetComponent<Image>();

    }

    public void Init(Item item)
    {
        this.item = item;
        if (item == null)
        {
            icon.sprite = null;
        }
        else { icon.sprite = item.Icon; }

    }


    public void OnClickCell()
    {
        if (item == null)
        {
            return;
        }
        //Удаляем выбраный элемент из массива
        GameManager.Instance.inventory.Items.Remove(item);

        //Создаем бафф
        Buff buff = new Buff
        {
            type = item.Type,
            additiveBonus = item.Value
        };
        //Получить бафф
        GameManager.Instance.inventory.BuffReciever.AddBuff(buff);

        // Если на нашь делегат подписаны какие нибудь методы то мы его вызываем
        if (OnUpdateCell != null)
        {
            OnUpdateCell();
        }

    }
}
