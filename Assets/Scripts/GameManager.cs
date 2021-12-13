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

    // Start is called before the first frame update
    void Start()
    {
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
