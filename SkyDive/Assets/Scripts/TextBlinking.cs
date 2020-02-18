using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinking : MonoBehaviour
{
    Text text;
    bool reverse;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        reverse = true;
    }
    
    void Update()
    {
        Color color = text.color;

        if(text.enabled)
        {
            if (reverse)
            {
                color.a -= 0.05f;
                text.color = color;
                if (text.color.a <= 0)
                {
                    reverse = false;
                }

            }
            else
            {
                color.a += 0.05f;
                text.color = color;
                if (text.color.a >= 1) reverse = true;
            }
        }
       
    }
}
