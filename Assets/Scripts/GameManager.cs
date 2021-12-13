using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas victoryMenu;

    public bool gameEnded;

    // Start is called before the first frame update
    void Awake()
    {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Victory()
    {
        gameEnded = true;
        victoryMenu.gameObject.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene("ProtoMain");
    }
}