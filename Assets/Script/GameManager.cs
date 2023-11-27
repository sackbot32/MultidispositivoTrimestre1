using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager gm;
    private int deadEnemies;
    public int deathRequirement;
    public bool requirement;
    public Text victoryText;
    public GameObject gameOver;
    public Text remainingEnemies;
    public GameObject titan;
    private void Awake()
    {
        gm = this;
        deadEnemies = 0;
        requirement = false;
        remainingEnemies.text = "RemainingEnemies: " + (deathRequirement - deadEnemies);
    }

    public void enemyDeath()
    {
        if (!requirement)
        {
            deadEnemies++;
            if (deadEnemies >= deathRequirement)
            {
                //Esto se usara para invocar el titan y limpiar el resto de enemigos
                requirement = true;
                foreach (EnemyHealth item in Object.FindObjectsOfType<EnemyHealth>())
                {
                    item.gameObject.SetActive(false);
                }
                titan.SetActive(true);
            }
            remainingEnemies.text = "RemainingEnemies: " + (deathRequirement - deadEnemies);
        }
    }

    public void Victory()
    {
        victoryText.text = "Victory";
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameOver.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameOver.SetActive(true);
    }
}
