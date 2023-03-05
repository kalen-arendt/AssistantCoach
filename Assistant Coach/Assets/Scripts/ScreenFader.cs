using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

	[SerializeField] protected Image fadePanel = null;


	FadeType fadeType;
	float duration;
	bool setActiveAfter;

	Color startColor;
	Color endColor;
	float progress;

	protected enum FadeType {FadeOut, FadeIn}


	void Awake ()
	{
		fadePanel.gameObject.SetActive(false);
	}

	protected void Fade (FadeType fadeType, float duration, bool setActiveAfter)
	{
		StopAllCoroutines();
		SetParameters(fadeType, duration, setActiveAfter);
		StartCoroutine(eFade());
	}

	void SetParameters (FadeType fadeType, float duration, bool setActiveAfter)
	{
		this.fadeType = fadeType;
		this.duration = duration;
		this.setActiveAfter = setActiveAfter;

		this.startColor = fadeType == FadeType.FadeOut ? Color.clear : Color.black;
		this.endColor = fadeType == FadeType.FadeOut ? Color.black : Color.clear;

		this.fadePanel.gameObject.SetActive(true);
		this.fadePanel.color = startColor;

		this.progress = 0;
	}

	IEnumerator eFade ()
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


	protected virtual IEnumerator AfterFading (FadeType fadeType){yield return null;}
}