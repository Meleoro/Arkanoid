using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Transform startImage;
    [SerializeField] private Transform tutorialImage;
    [SerializeField] private Transform quitImage;
    [SerializeField] private Transform retourImage;

    [SerializeField] private Transform cameraObjectPos;

    private bool isOntuto;


    public void OnButton(int button)
    {
        if (button == 1)
        {
            startImage.DOScale(new Vector3(1.3f, 1.3f, 1), 0.2f);
        }
        else if (button == 2)
        {
            tutorialImage.DOScale(new Vector3(1.3f, 1.3f, 1), 0.2f);
        }
        else if (button == 3)
        {
            quitImage.DOScale(new Vector3(1.3f, 1.3f, 1), 0.2f);
        }
        else
        {
            retourImage.DOScale(new Vector3(2f * 1.2f, 0.7f * 1.2f, 1), 0.2f);
        }
    }

    public void OutButton(int button)
    {
        if (button == 1)
        {
            startImage.DOScale(new Vector3(1f, 1f, 1), 0.2f);
        }
        else if (button == 2)
        {
            tutorialImage.DOScale(new Vector3(1f, 1f, 1), 0.2f);
        }
        else if (button == 3)
        {
            quitImage.DOScale(new Vector3(1f, 1f, 1), 0.2f);
        }
        else
        {
            retourImage.DOScale(new Vector3(2f, 0.7f, 1), 0.2f);
        }
    }



    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void OnTutorial()
    {
        isOntuto = !isOntuto;

        if (isOntuto)
            cameraObjectPos.DOMove(cameraObjectPos.transform.position + new Vector3(0, 0, -80), 2);
        
        else
            cameraObjectPos.DOMove(cameraObjectPos.transform.position + new Vector3(0, 0, 80), 2);
    }
}
