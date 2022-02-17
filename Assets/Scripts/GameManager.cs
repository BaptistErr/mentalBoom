using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas endGameMenu;

    public bool gameEnded;
    public bool enigmaFinished;

    float timeLeftBeforeRestart = 300.0f;

    // Start is called before the first frame update
    void Awake()
    {
        gameEnded = false;
        enigmaFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey)
        {
            timeLeftBeforeRestart -= Time.deltaTime;
            //Debug.Log(timeLeftBeforeRestart);

            if (timeLeftBeforeRestart <= 0)
            {
                Debug.Log("RESTART");
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
        gameEnded = true;
        if (victory)
        {
            endGameMenu.GetComponentInChildren<Text>().text = "VICTORY";
            endGameMenu.GetComponentInChildren<Text>().color = Color.green;
        }
        endGameMenu.gameObject.SetActive(true);
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelDesign");
        gameEnded = false;
    }
}