using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private InputField nameField;
    private const string playerName = "player_name"; // ключь для хранения имени игрока введенного в поле

    private void Start()
    {
        if (PlayerPrefs.HasKey(playerName))
        {
            nameField.text = PlayerPrefs.GetString(playerName);
        }
    }
    public void OnEndEditName()
    {
        PlayerPrefs.SetString(playerName, nameField.text);
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
