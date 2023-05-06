using System.Collections;

using UnityEngine;


public class SplashScreenFader : ScreenFader
{
	[SerializeField] private bool   loadNextScene   = true;
	[SerializeField] private bool   stateAfterFade  = true;
	[SerializeField] private float  fadeInDuration  = 1;
	[SerializeField] private float  timeBetween     = 0;
	[SerializeField] private float  fadeOutDuration = 1;
	[SerializeField] private LevelManager levelManager = null;

	private void Awake() { } //over-writes this so that the fade panel isn't turned off 

	private void OnEnable()
	{
		Fade(FadeType.FadeIn, fadeInDuration, stateAfterFade);
	}

	protected override IEnumerator AfterFading(FadeType fadeType)
	{
		if (fadeOutDuration == 0 || fadeType == FadeType.FadeOut) // if it only fades in, or if its fading out...
		{
			if (loadNextScene)
			{
				levelManager.LoadNextLevel();
				yield break;
			}

			if (!stateAfterFade)
			{
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