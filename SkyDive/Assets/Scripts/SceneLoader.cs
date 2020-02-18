using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public GameObject myGameObject;
    public GameObject gameManager;
    public bool isLoadingDone;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(myGameObject);
        }
        else
        {
            SceneLoader.instance = this;
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        gameManager.SetActive(false);
    }

    public void titleScene()
    {
        SceneManager.LoadScene("MainMenu");
        gameManager.SetActive(false);
    }

    public void stageSelectScene()
    {
        SceneManager.LoadScene("StageSelect");
        gameManager.SetActive(false);
    }

    public void playScene()
    {
        gameManager.SetActive(false);
        gameManager.SetActive(true);
        switch (GameManager.instance.curStageNum)
        {
            case StageNumber.NONE:
                break;
            case StageNumber.CUBE_1:
                SceneManager.LoadScene("Play_Test");           
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case StageNumber.CUBE_2:
                SceneManager.LoadScene("Play_Stage1");           
                Cursor.lockState = CursorLockMode.Locked;
                break;
            default:
                break;
        }

        
    }
}
