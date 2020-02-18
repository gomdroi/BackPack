using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectInfo : MonoBehaviour
{
    public StageNumber curStageNum;
    public Text stageName;
    float rayDistance;


    private void Start()
    {
        rayDistance = 15f;
    }

    void Update()
    {
        StageSelectRayCast();
    }

    void StageSelectRayCast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                Debug.Log(hit.transform.name);
                curStageNum = hit.collider.GetComponent<StageCube>().stageNum;
                GameManager.instance.curStageNum = curStageNum;
                switch(curStageNum)
                {                   
                    case StageNumber.CUBE_1:
                        stageName.text = "STAGE TEST";
                        break;
                    case StageNumber.CUBE_2:
                        stageName.text = "STAGE BLUE";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
