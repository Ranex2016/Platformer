using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterController : MonoBehaviour
{
    [SerializeField] private PressedButton leaft;
    [SerializeField] private PressedButton right;
    [SerializeField] private Button fire;
    [SerializeField] private Button jump;

    public PressedButton Leaft{ get{return leaft;}}
    public PressedButton Right{ get{return right;}}
    public Button Fire{ get{return fire;}}
    public Button Jump{ get{return jump;}}
    void Start()
    {

    }
}
