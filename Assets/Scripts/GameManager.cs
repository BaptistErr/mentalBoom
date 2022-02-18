using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas endGameMenu;

    public static bool HasGameEnded;
    public bool enigmaFinished;

    float timeLeftBeforeRestart = 60.0f;

    // Start is called before the first frame update
    void Awake()
    {
        HasGameEnded = false;
        enigmaFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Replay();
        }
        if (!Input.anyKey)
        {
            timeLeftBeforeRestart -= Time.deltaTime;
            //Debug.Log(timeLeftBeforeRestart);

            if (timeLeftBeforeRestart <= 0)
            {
                Replay();
            }
        }
        if (Input.anyKey)
        {
            timeLeftBeforeRestart = 300.0f;
        }
    }

    public void GameEnded(bool victory)
    {
        Time.timeScale = 0f;
        HasGameEnded = true;
        if (victory)
        {
            endGameMenu.GetComponentInChildren<Text>().text = "VICTORY";
            endGameMenu.GetComponentInChildren<Text>().color = Color.green;
        }
        endGameMenu.gameObject.SetActive(true);
        
        Cursor.visible = true;
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.LoadScene("LevelDesign");
        HasGameEnded = false;
    }
}