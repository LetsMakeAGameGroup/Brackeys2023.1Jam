using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TMP_Text dialogueText;

    private void Start()
    {
        StartFadeInAndOut(2, 5);
    }

    public void StartFadeIn(float durationToFadeIn) 
    {
        StartCoroutine(FadeTextIn(durationToFadeIn));
    }

    public void StartFadeOut(float durationToFadeOut) 
    {
        StartCoroutine(FadeTextOut(durationToFadeOut));
    }

    public void StartFadeInAndOut(float durationToFadeInOut, float durationInBetween)
    {
        StartCoroutine(FadeInAndOut(durationToFadeInOut, durationInBetween));
    }

    IEnumerator FadeTextIn(float durationToFadeIn) 
    {
        if (!dialogueText.gameObject.activeSelf) 
        {
            dialogueText.gameObject.SetActive(true);
        }

        float timeElapse = 0;
        float currentAlpha = 0;

        while (timeElapse < durationToFadeIn)
        {
            Color newColor = dialogueText.color;
            newColor.a = Mathf.Lerp(currentAlpha, 1, timeElapse / durationToFadeIn);
            timeElapse += Time.deltaTime;
            dialogueText.color = newColor;

            yield return null;
        }

        yield return null;
    }

    IEnumerator FadeInAndOut(float durationToFadeInOut,  float durationToWaitBetween) 
    {
        StartCoroutine(FadeTextIn(durationToFadeInOut));

        yield return new WaitForSeconds(durationToFadeInOut + durationToWaitBetween);

        StartCoroutine(FadeTextOut(durationToFadeInOut));

        yield return null;
    }

    IEnumerator FadeTextOut(float durationToFadeOut)
    {
        float timeElapse = 0;
        float currentAlpha = dialogueText.color.a;

        while (timeElapse < durationToFadeOut)
        {
            Color newColor = dialogueText.color;
            newColor.a = Mathf.Lerp(currentAlpha, 0, timeElapse / durationToFadeOut);
            timeElapse += Time.deltaTime;
            dialogueText.color = newColor;

            yield return null;
        }

        dialogueText.gameObject.SetActive(false);

        yield return null;
    }

}
