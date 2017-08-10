using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Text textObject;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private float delayBeforeFade, imageFadeTime, imageMaxAlpha, sizeDifference, fadeInTime;

    private Vector3 originalSize, startSize;

    // Use this for initialization
    void Start()
    {
        restartButton.gameObject.SetActive(false);
        originalSize = textObject.gameObject.transform.localScale;
        startSize = new Vector3(originalSize.x - sizeDifference, originalSize.y - sizeDifference, originalSize.z);
        textObject.gameObject.transform.localScale = startSize;
    }

    public void StartFade()
    {
        StartCoroutine(FadeInBackground());
    }

    IEnumerator FadeInBackground()
    {
        backgroundImage.gameObject.SetActive(true);
        Color temp = backgroundImage.color;
        temp.a = 0;
        backgroundImage.color = temp;

        for (float i = 0; i <= imageMaxAlpha; i += imageMaxAlpha / 50)
        {
            temp.a = i/255;
            backgroundImage.color = temp;
            yield return new WaitForSeconds(0.001f);
        }

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        textObject.gameObject.SetActive(true);
        Color temp = textObject.color;
        temp.a = 0;
        textObject.color = temp;
        yield return new WaitForSeconds(delayBeforeFade);
        float i;
        float alphaStep = 1 / sizeDifference;
        for (i = 0; i <= sizeDifference; i += sizeDifference / (fadeInTime * 10))
        {
            //if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Q))
            //{
            //    i = sizeDifference;
            //}
            
            temp.a = i * alphaStep;
            textObject.color = temp;
            textObject.gameObject.transform.localScale = new Vector3(startSize.x + i, startSize.y + i, startSize.z);
            yield return new WaitForSeconds(0.05f);
        }
        restartButton.gameObject.SetActive(true);
    }
}
