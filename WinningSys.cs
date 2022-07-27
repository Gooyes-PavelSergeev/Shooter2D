using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningSys : MonoBehaviour
{
    public  GameObject player;
    public  GameObject enemy;

    private int playerWinNum;
    private int enemyWinNum;
    // Start is called before the first frame update
    void Start()
    {
        playerWinNum = PlayerPrefs.GetInt("PlayerScore");
        enemyWinNum = PlayerPrefs.GetInt("EnemyScore");

        PlayerPrefs.SetInt("PlayerScore", playerWinNum);
        PlayerPrefs.SetInt("EnemyScore", enemyWinNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("PlayerScore") > 10 || PlayerPrefs.GetInt("EnemyScore") > 10)
        {
            PlayerPrefs.DeleteKey("PlayerScore");
            PlayerPrefs.DeleteKey("EnemyScore");
            PlayerPrefs.SetInt("PlayerScore", 0);
            PlayerPrefs.SetInt("EnemyScore", 0);
        }
        if (enemy == null)
        {
            playerWinNum++;
            PlayerPrefs.DeleteKey("PlayerScore");
            PlayerPrefs.SetInt("PlayerScore", playerWinNum);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (player == null)
        {
            enemyWinNum++;
            PlayerPrefs.DeleteKey("EnemyScore");
            PlayerPrefs.SetInt("EnemyScore", enemyWinNum);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
