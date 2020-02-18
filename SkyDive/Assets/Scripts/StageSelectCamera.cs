using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectCamera : MonoBehaviour
{
    bool isRotateDone;
    bool isRightRotateStart;
    bool isLeftRotateStart;
    Vector3 targetRotation;

    private void Start()
    {
        isRotateDone = true;
        isRightRotateStart = false;
        isLeftRotateStart = false;
        targetRotation = Vector3.zero;
    }

    private void Update()
    {
        //입력
        if (Input.GetKeyDown(KeyCode.D))
        {
            isRightRotateStart = true;           
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            isLeftRotateStart = true;
        }
        //우측 회전을 시작
        if (isRightRotateStart)
        {
            rotateRight();
        }
        else if (isLeftRotateStart)
        {
            rotateLeft();
        }

    }


    void rotateRight()
    {
        //로테이션 정보 입력
        if (isRotateDone)
        {
            targetRotation = transform.rotation.eulerAngles;
            targetRotation.y = targetRotation.y += 45f;

            isRotateDone = false;
        }
        //돌린다
        else if (!isRotateDone)
        {
            transform.Rotate(0, 2f, 0);

            if(transform.rotation.eulerAngles.y >= 359)
            {
                targetRotation.y = 0;
            }

            if (transform.rotation.eulerAngles.y >= targetRotation.y)
            {
                transform.rotation = Quaternion.Euler(targetRotation);
                isRightRotateStart = false;
                isRotateDone = true;
            }
        }      
    }

    void rotateLeft()
    {
        //로테이션 정보 입력
        if (isRotateDone)
        {
            targetRotation = transform.rotation.eulerAngles;
            if(targetRotation.y == 0)
            {
                targetRotation.y = 360 - 45f;
            }
            else
            targetRotation.y = targetRotation.y -= 45f;

            isRotateDone = false;
        }
        //돌린다
        else if (!isRotateDone)
        {
            transform.Rotate(0, -2f, 0);           

            if(transform.rotation.eulerAngles.y <= 1.5f)
            {
                targetRotation.y = 0;
            }
            if (transform.rotation.eulerAngles.y <= targetRotation.y || targetRotation.y == 0)
            {
                transform.rotation = Quaternion.Euler(targetRotation);
                isLeftRotateStart = false;
                isRotateDone = true;
            }
        }
    }

}
