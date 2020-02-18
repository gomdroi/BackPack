using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    PlayerCtrl thePC;
    [SerializeField] float shakeAmount = 0;

    private void Awake()
    {
        thePC = FindObjectOfType<PlayerCtrl>();
    }

    void Update()
    {
        shake();
    }
    

    void shake()
    {
        //float downSpeedPercentage = ((thePC.velocity.y / thePC.maxVelocity) * 100);
        //float shakeProgress = ((shakeAmount / 100) * downSpeedPercentage);
        
        if (!thePC.isGrounded)
        transform.localRotation = Quaternion.Euler(Random.insideUnitSphere * ((shakeAmount / 100) * (thePC.velocity.y / thePC.maxVelocity) * 100));       
    }
}
