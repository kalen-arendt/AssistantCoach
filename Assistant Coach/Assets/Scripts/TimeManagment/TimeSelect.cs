using UnityEngine;
using UnityEngine.UI;

public class TimeSelect : MonoBehaviour
{
	[SerializeField] Text localDisplay = null;
	[SerializeField] GameObject Cover = null;
	[SerializeField] RectTransform proportionalDisplay = null;
	[SerializeField] Text percentageText = null;

	bool On;

	int currentTime = 20;
	int temporaryTime;
	int index = -1;

	public int Index { set {index = value;} get {return index;}}
	public int Time { set {currentTime = value;} get {return currentTime;}}


	const int TIME_INCREMENT = 5;

	public delegate void TimeChanged (int index, int time);
	public static event TimeChanged onTimeChanged;


	void Awake ()
	{
		temporaryTime = currentTime;
	}

	// Button
	public void ChangeTime (bool increase)
	{
		int x = increase ? TIME_INCREMENT : -TIME_INCREMENT;
		int new_time = Mathf.Clamp(x + currentTime, 0, 90);
		UpdateTime (new_time);
	}

	// Button
	public void SetCoverActive (bool active)
	{
		On = !active;

		if (Cover) { Cover.SetActive (active); }

		if (On)
		{
			UpdateTime (temporaryTime);
		}
		else
		{
			temporaryTime = currentTime;
			UpdateTime (0);
		}
	}

	void UpdateTime (int newTime)
	{
		if (index != -1)
		{
			currentTime = newTime;

			if (onTimeChanged != null)
			{
				onTimeChanged (index, currentTime);
			}
		}
	}

	public void SetTime (int newTime)
	{
		currentTime = newTime;
	}

	public void ShowValue ()
	{
		localDisplay.text = currentTime.ToString ();

		if (currentTime != 0)
		{
			float percent = Mathf.Round(100f * (float)currentTime / (float)Timeline.TotalTime);
			percentageText.text = (percent + "%");
		}
		else
		{
			percentageText.text = "";
		}
	}

	public void ResizeToPercentage (float min, float max)
	{
		proportionalDisplay.anchorMin = new Vector2 (min, 0);
		proportionalDisplay.anchorMax = new Vector2 (max, 1);
	}		
}