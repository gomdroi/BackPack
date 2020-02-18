using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject myGameObject;
    public Text Thugs;
    public Text Tkissed;
    public Text Tparachute;
    public int hugs;
    public int kissed;
    public float diveTime;
    public bool isDead;
    public bool isSucces;

    public Text hugsScore;
    public Text kissedScore;
    public Text landingScore;
    public Text timeScore;
    public Text totalScore;
    public GameObject scoreBoard;
    public GameObject scoreBoardButtons;
    public Image scoreBar;

    public int huggedBuilding;
    private bool isHugged;
    private bool isGameOver;

    public StageNumber curStageNum;

    //마우스 센시
    public float mouseSensitivity = 100f;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(myGameObject);
        }
        else
        {
            GameManager.instance = this;
        }

        DontDestroyOnLoad(this);
    }

    //void Start()
    //{
    //    hugs = 0;
    //    kissed = 0;
    //    diveTime = 0;
    //    huggedBuilding = 0;
    //    isHugged = false;
    //    isDead = false;
    //    isSucces = false;
    //    isGameOver = false;
    //}

    private void OnEnable()
    {
        hugs = 0;
        kissed = 0;
        diveTime = 0;
        huggedBuilding = 0;
        isHugged = false;
        isDead = false;
        isSucces = false;
        isGameOver = false;
        scoreBoard.gameObject.SetActive(false);
        scoreBoardButtons.gameObject.SetActive(false);

        Thugs.text = "Hugs ";
        Tkissed.text = "Kisses ";
        kissedScore.text = "";
        hugsScore.text = "";
        landingScore.text = "";
        timeScore.text = "";
        totalScore.text = "";
    }

    private void Update()
    {
        if(!isDead)
        {
            hugsPointCount();
            kissedPointCount();
        }
        
        if(isDead || isSucces)
        {
            if(!isGameOver)
            {
                scoreBoard.gameObject.SetActive(true);
                StopCoroutine("hugsCount");
                StartCoroutine("pointCalc");
                isGameOver = true;
            }
        }
    }

    IEnumerator hugsCount()
    {
        while (true)
        {
            hugs += huggedBuilding;
            Thugs.text = "Hugs " + hugs.ToString();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void hugsPointCount()
    {
        if (huggedBuilding >= 1)
        {
            if (!isHugged)
            {
                StartCoroutine("hugsCount");
                isHugged = true;
            }
        }
        else if (huggedBuilding <= 0)
        {
            StopCoroutine("hugsCount");
            isHugged = false;
        }
    }

    void kissedPointCount()
    {
        if (kissed >= 1)
        {
            Tkissed.text = "Kisses " + kissed.ToString();
        }
    }

    IEnumerator pointCalc()
    {
        int kissedMultiplier = 1000;
        kissedScore.text = "Kissed Score : " + kissed + " X " + kissedMultiplier + " = " + kissed * kissedMultiplier;

        yield return new WaitForSeconds(0.5f);

        int hugsMultiplier = 100;
        hugsScore.text = "Hugs Score : " + hugs + " X " + hugsMultiplier + " = " + hugs * hugsMultiplier;

        yield return new WaitForSeconds(0.5f);

        int landing = 3000;
        int landingMultiplier = 0;
        if (isSucces) landingMultiplier = 4;

        landingScore.text = "Landing Score : " + landing + " X " + landingMultiplier + " = " + landing * landingMultiplier;

        yield return new WaitForSeconds(0.5f);

        int timeMultiplier = 10;
        timeScore.text = "Time Score : " + diveTime + " X " + timeMultiplier + " = " + diveTime * timeMultiplier;

        yield return new WaitForSeconds(0.5f);

        scoreBar.enabled = true;

        yield return new WaitForSeconds(1f);

        float total = (kissed * kissedMultiplier) + (hugs * hugsMultiplier) + (landing * landingMultiplier) + (diveTime * timeMultiplier);
        totalScore.text = "Total Score : " + total;

        yield return new WaitForSeconds(0.2f);

        scoreBoardButtons.gameObject.SetActive(true);
        isGameOver = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
