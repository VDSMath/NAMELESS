﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextHandle : MonoBehaviour {

    [SerializeField]
    private Text textObject;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private float delayBeforeFade, sizeDifference, fadeInTime;

    private Vector3 originalSize, startSize;

    // Use this for initialization
    void Start () {
        startButton.gameObject.SetActive(false);
        originalSize = textObject.gameObject.transform.localScale;
        startSize = new Vector3(originalSize.x - sizeDifference, originalSize.y - sizeDifference, originalSize.z);
        textObject.gameObject.transform.localScale = startSize;
        StartCoroutine(FadeIn());
	}

    IEnumerator FadeIn()
    {
        Color temp = textObject.color;
        temp.a = 0;
        textObject.color = temp;
        yield return new WaitForSeconds(delayBeforeFade);
        float i;
        float alphaStep = 1 / sizeDifference;
        for (i = 0; i <= sizeDifference; i += sizeDifference / (fadeInTime * 10))
        {
            if(Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Q))
            {
                i = sizeDifference;
            }

            //if(i >= 0.1)
                //textObject.text = "CHATUBALESS";
            temp.a = i * alphaStep;
            textObject.color = temp;
            textObject.gameObject.transform.localScale = new Vector3(startSize.x + i, startSize.y + i, startSize.z);
            yield return new WaitForSeconds(0.05f);
        }
        startButton.gameObject.SetActive(true);
    }
}
