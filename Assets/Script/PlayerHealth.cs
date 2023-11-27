using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public Text healthIndicator;
    public GameObject pauseMenu;
    public InputActionReference pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseButton.action.WasPerformedThisFrame())
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        updateText();
        if(currentHealth < 0)
        {
            pauseMenu.SetActive(false);
            GameManager.gm.GameOver();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void updateText()
    {
        healthIndicator.text = "health: " + currentHealth;
    }
}
