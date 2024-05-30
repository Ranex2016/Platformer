using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singletone
    public static GameManager Instance { get; private set; } // Объявляем синглтон с авто свойствами
    #endregion

    public Dictionary<GameObject, Health> healthContainer; // Словарик для хранения объектов здоровья
    public Dictionary<GameObject, Coin> coinContainer; // Словарик для манеток
    public Dictionary<GameObject, BuffReciever> buffRecieverConteiner; // Словарик для Бафов


    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverConteiner = new Dictionary<GameObject, BuffReciever>();
    }

    // Пауза
    public void OnClickPause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
