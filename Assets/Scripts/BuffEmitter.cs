using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEmitter : MonoBehaviour
{
    [SerializeField] private Buff buff;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверим что в словарике бафов есть то с чем мы столкнулись
        if (GameManager.Instance.buffRecieverConteiner.ContainsKey(other.gameObject))
        {
            var reciever = GameManager.Instance.buffRecieverConteiner[other.gameObject];
            reciever.AddBuff(buff);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Проверим что мы вышли из тригерной зоны
        if(GameManager.Instance.buffRecieverConteiner.ContainsKey(other.gameObject)){
            var reciever = GameManager.Instance.buffRecieverConteiner[other.gameObject];
            reciever.RemoveBuff(buff);
        }
    }
}

[System.Serializable]
public class Buff
{
    public BuffType type;
    public float additiveBonus;
    public float multipleBonuse;
}

public enum BuffType : byte
{
    Damage, Force, Armore
}
