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
    public Dictionary<GameObject, ItemComponent> itemConteiner; // Контейнер для бутылочек
    [HideInInspector] public PlayerInventory inventory;
    public ItemBase itemDataBase;

    [SerializeField] private GameObject inventoryPanal;


    private void Awake()
    {
        Instance = this; // Инициализация синглтона
        // Инициализация контейнеров
        healthContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverConteiner = new Dictionary<GameObject, BuffReciever>();
        itemConteiner = new Dictionary<GameObject, ItemComponent>();
        inventory = PlayerInventory.Instance; // получаем ссылку на инвентарь (синглтон)

    }

    // Пауза
    public void OnClickPause()
    {
        if (Time.timeScale > 0)
        {
            inventoryPanal.gameObject.SetActive(true);
            Time.timeScale = 0;

        }
        else
        {
            inventoryPanal.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
