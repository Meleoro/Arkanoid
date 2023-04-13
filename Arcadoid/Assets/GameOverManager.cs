using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Transform retry;
    public Transform mainMenu;

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    

    public void OnButton(int currentButton)
    {
        if (currentButton == 1)
        {
            retry.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f).SetUpdate(true);
        }
        else
        {
            mainMenu.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f).SetUpdate(true);
        }
    }

    public void OutButton(int currentButton)
    {
        if (currentButton == 1)
        {
            retry.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f).SetUpdate(true);
        }
        else
        {
            mainMenu.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f).SetUpdate(true);
        }
    }
}
