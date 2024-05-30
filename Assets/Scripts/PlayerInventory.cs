using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int coinsCount;
    public static PlayerInventory Instance { get; set; } // Создание синглтона

    // Гетер и сетер для CoinsCount
    public int CoinsCount
    {
        get { return coinsCount; }
        set
        {
            if (value >= 1) { coinsCount = value; }
        }
    }

    [SerializeField] private Text coinsText;
    public Text CoinsText {
        get { return coinsText; }
        set {coinsText = value;}
    }
    
    private void Awake()
    {
        Instance = this; // Инициализация синглтона
    }
}
