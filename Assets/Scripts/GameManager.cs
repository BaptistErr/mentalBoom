using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isInputEnabled;
    /// <summary> Used to disable inputs when the character dies </summary>
    public bool IsInputEnabled
    {
        get => _isInputEnabled;
        set => _isInputEnabled = value;
    }

    [SerializeField] private bool _characterIsAlive;
    /// <summary> True when character is still alive </summary>
    public bool CharacterIsAlive
    {
        get => _characterIsAlive;
        set => _characterIsAlive = value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        CharacterIsAlive = true;
        IsInputEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Victory()
    {
        
    }
}