using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageNumber
{
    NONE,
    CUBE_1,
    CUBE_2,
    CUBE_3
}

public class StageCube : MonoBehaviour
{

    Vector3 nextShape;
    Vector3 jiggleShape;
    bool isJiggle;
    float rndNum;
    float desRotX;
    float desRotZ;
    public float jiggleAmount;
    bool reverse;
    bool isJiggleDone;
    float jiggleDelay;
    float jiggleDelayMax;
    public StageNumber stageNum;
    public bool isCubeOpen;
    
   
    void Start()
    {
        rndNum = 0;
        desRotX = 0;
        desRotZ = 0;
        jiggleDelay = 0;
        jiggleDelayMax = 0;
        jiggleAmount = 1;
        isJiggle = false;
        reverse = false;
        isJiggleDone = false;
    }
    

    void Update()
    {
        if(isCubeOpen)
        {
            jiggleCubeX();
            rotateCube();
        }
    }

    void rotateCube()
    {
        //큐브의 기본 회전
        transform.Rotate(new Vector3(0, 0.1f, 0));
        
        //큐브의 지금 모양 저장
        nextShape = transform.rotation.eulerAngles;

        //변하는 큐브의 좌표 변경
        nextShape.x = desRotX;
        nextShape.z = desRotZ;

        //반영
        transform.rotation = Quaternion.Euler(nextShape);
    }

    void jiggleCubeX()
    {
        //흔들림 시작
        if (!isJiggle)
        {
            //랜덤한 흔들릴 세기와 방향          
            while(true)
            {
                rndNum = Random.Range(-15f, 15f);
                if (!(Mathf.Abs(rndNum) < 10f)) break;      
            }          
            //랜덤한 흔들리는 간격
            jiggleDelayMax = Random.Range(2f, 3f);
            //흔들리는 방향에 따라서 음수,양수 반환
            jiggleAmount = (rndNum >= 0) ? Mathf.Abs(jiggleAmount) : (-1) * Mathf.Abs(jiggleAmount);
            isJiggle = true;
        }
        //흔들리는 도중
        else if (isJiggle)
        {
            //다 흔들렸는지 체크
            if (!isJiggleDone)
            {
                desRotX += jiggleAmount;
                if (Mathf.Abs(desRotX) >= Mathf.Abs(rndNum) && !reverse)
                {
                    jiggleAmount = jiggleAmount * -1;
                    reverse = true;
                }

                else if (Mathf.Abs(desRotX) - Mathf.Abs(jiggleAmount) <= 0 && reverse)
                {
                    desRotX = 0;
                    desRotZ = 0;
                    isJiggleDone = true;
                }
            }
            //흔들기 루틴이 다 돌았을 경우
            else if (isJiggleDone)
            {
                //다음 흔들기까지 JiggleDelayMax초               
                jiggleDelay += Time.deltaTime;
                if (jiggleDelay >= jiggleDelayMax)
                {
                    isJiggle = false;
                    reverse = false;
                    isJiggleDone = false;
                    jiggleDelay = 0;                 
                }
            }
        }
    }
}

