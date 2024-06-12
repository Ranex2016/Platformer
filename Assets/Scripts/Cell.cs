using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
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
        icon.sprite = item.Icon;
    }
}
