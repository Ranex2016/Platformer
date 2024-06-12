using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Databases/Items")]
public class ItemBase : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Item> items;
    [SerializeField] private Item currentItem;
    private int currentIndex;

    public void CreateItem()
    {
        if (items == null)
        {
            items = new List<Item>();
        }
        Item item = new Item();
        items.Add(item);
        currentItem = item;
        currentIndex = items.Count - 1;

    }

    //Получение Item по его ID
    public Item GetItemOfID(int id)
    {
        // foreach (var item in items)
        // {
        //     if (item.ID == id)
        //     {
        //         return item;
        //     }
        // }
        // return null;
        return items.Find(x => x.ID == id); // поиск по коллекции при помощи лямбды
    }

    public void RemoveItems()
    {
        if (items == null) { return; }
        if (currentItem == null) { return; }

        items.Remove(currentItem);

        // Проверим что в БД есть объекты
        if (items.Count > 0)
        {
            currentItem = items[0];
        }
        // Иначе создадим пустой объект
        else { CreateItem(); }

        currentIndex = 0;
    }

    public void NextItem()
    {
        if (currentIndex + 1 < items.Count)
        {
            currentIndex++;
            currentItem = items[currentIndex];
        }
    }

    public void PrevItem()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            currentItem = items[currentIndex];
        }
    }
}

[System.Serializable]
public class Item
{
    [SerializeField] private int id;
    public int ID { get { return id; } }

    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] private string description;
    public string Description
    { get { return description; } }

    [SerializeField] private BuffType type;
    public BuffType Type
    { get { return type; } }

    [SerializeField] private float value;
    public float Value
    { get { return value; } }

    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } }
}
