using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int coinsCount;
    public static PlayerInventory Instance { get; set; } // Создание синглтона

    // Гетер и сетер для oinsCount
    public int CoinsCount
    {
        get { return coinsCount; }
        set { coinsCount = value; }
    }
    private void Awake()
    {
        Instance = this; // Инициализация синглтона
    }
}
