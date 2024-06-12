using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int coinsCount;
    // Гетер и сетер для CoinsCount
    public int CoinsCount
    {
        get { return coinsCount; }
        set { if (value >= 1) { coinsCount = value; } }
    }
    [SerializeField] private Text coinsText;
    public Text CoinsText
    {
        get { return coinsText; }
        set { coinsText = value; }
    }
    public List<Item> items; // !!! Нужно сделать приватной и переделать доступ через свойсво !!!
    public List<Item> Items { get { return items; } }

    // Создание синглтона
    public static PlayerInventory Instance { get; set; }

    private void Awake()
    {
        Instance = this; // Инициализация синглтона
    }
    private void Start()
    {
        items = new List<Item>();
    }
}
