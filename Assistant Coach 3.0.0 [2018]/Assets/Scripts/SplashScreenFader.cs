using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SplashScreenFader : ScreenFader
{
	[SerializeField] bool 	loadNextScene 	= true;
	[SerializeField] bool	stateAfterFade	= true;
	[SerializeField] float 	fadeInDuration 	= 1;
	[SerializeField] float 	timeBetween 	= 0;
	[SerializeField] float 	fadeOutDuration	= 1;
	[SerializeField] LevelManager levelManager = null;


	void Awake(){} //over-writes this so that the fade panel isn't turned off 

	void OnEnable ()
	{
		Fade(FadeType.FadeIn, fadeInDuration, stateAfterFade);
	}

	protected override IEnumerator AfterFading(FadeType fadeType)
	{
		if (fadeOutDuration == 0 || fadeType == FadeType.FadeOut) // if it only fades in, or if its fading out...
		{
			if (loadNextScene) {
				levelManager.LoadNextLevel();
				yield break;
			}

			if (!stateAfterFade) {
				Destroy(gameObject);
			}

			yield break;
		}
		else
		{
			yield return new WaitForSecondsRealtime(timeBetween);
			Fade(FadeType.FadeOut, fadeOutDuration, stateAfterFade);
		}		
	}
}