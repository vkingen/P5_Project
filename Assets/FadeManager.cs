using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image imageToFade;
    public float fadeDuration;
    [SerializeField] private Color fadeColor;
    [SerializeField] private bool fadeInOnStart = true;


    public UnityEvent whenFadedOut;

    private void Start()
    {
        if (fadeInOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        Fade(1f, 0f, false);
    }

    public void FadeOut()
    {
        Fade(0f, 1f, false);
    }

   


    private void Fade(float fadeIn, float fadeOut, bool isSceneTransition)
    {
        StartCoroutine(FadeRoutine(fadeIn, fadeOut, isSceneTransition));
    }




    IEnumerator FadeRoutine(float fadeIn, float fadeOut, bool isSceneTransition)
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(fadeIn, fadeOut, timer / fadeDuration);

            imageToFade.color = newColor;

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = fadeOut;

        imageToFade.color = newColor2;

        if (isSceneTransition)
        {
            whenFadedOut.Invoke();
        }
    }

}
