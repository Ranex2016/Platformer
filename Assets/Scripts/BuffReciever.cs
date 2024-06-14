using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffReciever : MonoBehaviour
{
    private List<Buff> buffs;
    public Action OnBuffChanged;

    private void Start()
    {
        buffs = new List<Buff>();
        // При старте добавляем себя в словарь бафов
        GameManager.Instance.buffRecieverConteiner.Add(gameObject, this);
    }

    // Добавление баффа
    public void AddBuff(Buff buff)
    {
        // Проверим что баффа еще нет в листе
        if (!buffs.Contains(buff))
        {
            //добавляем бафф
            buffs.Add(buff);
        }

        // Проверим что наш Action не пуст, т.е. в нем существуют методы которые подписаны на этот делегат то мы запускаем его. 
        if (OnBuffChanged != null)
        {
            OnBuffChanged();
        }
    }

    // Удаление бафа
    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))
        {
            buffs.Remove(buff);
        }

        if (OnBuffChanged != null)
        {
            OnBuffChanged();
        }

    }

}
