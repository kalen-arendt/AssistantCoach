using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using static FadeType;

public class ScreenFader : MonoBehaviour {

    [SerializeField] protected Image fadePanel = null;


    FadeType fadeType;
    float duration;
    bool setActiveAfter;

    Color startColor;
    Color endColor;
    float progress;


    void Awake ()
    {
        fadePanel.gameObject.SetActive(false);
    }

    protected void Fade (FadeType fadeType, float duration, bool setActiveAfter)
    {
        StopAllCoroutines();
        SetParameters(fadeType, duration, setActiveAfter);
        StartCoroutine(Fade());
    }

    void SetParameters (FadeType fadeType, float duration, bool setActiveAfter)
    {
        this.fadeType = fadeType;
        this.duration = duration;
        this.setActiveAfter = setActiveAfter;

        startColor = fadeType == FadeOut ? Color.clear : Color.black;
        endColor = fadeType == FadeOut ? Color.black : Color.clear;

        fadePanel.gameObject.SetActive(true);
        fadePanel.color = startColor;

        progress = 0;
    }

    IEnumerator Fade ()
    {
        while (progress < 1)
        {
            progress = Mathf.Clamp(progress + Time.deltaTime, 0, 1);
            fadePanel.color = Color.Lerp(startColor, endColor, progress / duration);
            yield return new WaitForEndOfFrame();
        }

        fadePanel.gameObject.SetActive(setActiveAfter);
        StartCoroutine(AfterFading(fadeType));
    }


    protected virtual IEnumerator AfterFading(FadeType fadeType)
    {
        yield return null;
    }
}
