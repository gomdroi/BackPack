using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPoint : MonoBehaviour
{
    public bool isKissedTrigger;
    public bool isHuggedTrigger;
    public Material kissedMaterial;   

    void Start()
    {     
        isHuggedTrigger = false;
        isKissedTrigger = false;
    }

    //키스 판정
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Proximity Sensor")
        {
            
            if (isKissedTrigger == true) return;
            else
            {
                isKissedTrigger = true;
                GameManager.instance.kissed += 1;
            }             
        }
    }

    //허그 판정
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Proximity Sensor")
        {
            if (isHuggedTrigger == true) return;
            else
            {
                isHuggedTrigger = true;
                GameManager.instance.huggedBuilding += 1;
            }
        }
    }

    //허그 벗어남
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Proximity Sensor")
        {
            if(isHuggedTrigger == true)
            {
                isHuggedTrigger = false;
                GameManager.instance.huggedBuilding -= 1;
            }         
        }
    }

    void Update()
    {
        if(isKissedTrigger)
        {
            if(this.GetComponent<MeshRenderer>() != null)
            {
                this.GetComponent<MeshRenderer>().material = kissedMaterial;
            }
            else
            {
                Renderer[] renderers = GetComponentsInChildren<Renderer>();
                
                foreach (var renderer in renderers)
                {

                    renderer.material = kissedMaterial;

                }
            }         
        }      
    }
}
