using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiveButton : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(()=> { SceneLoader.instance.playScene(); });
    }
}
