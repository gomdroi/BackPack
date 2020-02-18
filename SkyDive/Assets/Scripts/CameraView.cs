using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    float xRotation = 0f;

    void Update()
    {
        if (GameManager.instance.isDead || GameManager.instance.isSucces) return;

        float mouseY = Input.GetAxis("Mouse Y") * GameManager.instance.mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    
}
