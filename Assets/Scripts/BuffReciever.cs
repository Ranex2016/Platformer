using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffReciever : MonoBehaviour
{
    private List<Buff> buffs;

    private void Start()
    {
        buffs = new List<Buff>();
        // При старте добавляем себя в словарь бафов
        GameManager.Instance.buffRecieverConteiner.Add(gameObject, this);
    }

    public void AddBuff(Buff buff)
    {
        // Проверим что баффа еще нет в листе
        if (!buffs.Contains(buff))
        {
            //добавляем бафф
            buffs.Add(buff);
        }
    }

    public void RemoveBuff(Buff buff){
        if(buffs.Contains(buff)){
            buffs.Remove(buff);
        }
    }

}
