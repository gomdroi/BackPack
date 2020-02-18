using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class parachutePoint : MonoBehaviour
{
    PlayerCtrl thePC;

    private void Awake()
    {
        thePC = FindObjectOfType<PlayerCtrl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Proximity Sensor")
        {
            GameManager.instance.Tparachute.enabled = true;
        }
    }

    private void Update()
    {
        if (thePC.isGrounded)
        {
            GameManager.instance.Tparachute.enabled = false;
        }
    }

}
