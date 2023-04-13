using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public GameObject pauseObejct;
    
    public Transform reprendre;
    public Transform mainMenu;
        
    
    public bool canPause;
    private bool pauseActive;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        pauseObejct.SetActive(false);

        canPause = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseActive)
                PauseGame();
            
            else
                QuitPause();
        }
    }


    public void PauseGame()
    {
        if (canPause)
        {
            Time.timeScale = 0;
            pauseActive = true;

            pauseObejct.SetActive(true);
        }
    }

    public void QuitPause()
    {
        Time.timeScale = 1;
        pauseActive = false;
        
        pauseObejct.SetActive(false);
    }


    public void Mainmenu()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene("MainMenu");
    }
    
    
    
    public void OnButton(int currentButton)
    {
        if (currentButton == 1)
        {
            reprendre.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetUpdate(true);
        }
        else
        {
            mainMenu.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetUpdate(true);
        }
    }

    public void OutButton(int currentButton)
    {
        if (currentButton == 1)
        {
            reprendre.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetUpdate(true);
        }
        else
        {
            mainMenu.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetUpdate(true);
        }
    }
}
