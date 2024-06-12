using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemBase))]
public class ItemBaseEditor : Editor
{
    private ItemBase itemBase;

    private void Awake()
    {
        itemBase = (ItemBase)target; // присваиваем ссылку которую хранит надстройка [CustomEditor(typeof (ItemBase))]
    }
    public override void OnInspectorGUI()
    {
        // Сдесь происходит описание разметки, это не условия.
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("New Item"))
        {
            itemBase.CreateItem();
        }

        if (GUILayout.Button("Remove"))
        {
            itemBase.RemoveItems();
        }

        if (GUILayout.Button("<="))
        {
            itemBase.PrevItem();
        }

        if (GUILayout.Button("=>"))
        {
            itemBase.NextItem();
        }

        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
