using UnityEngine;
using UnityEngine.UI;

public class TimeSelect : MonoBehaviour
{
	[SerializeField] Text localDisplay = null;
	[SerializeField] GameObject cover = null;
	[SerializeField] RectTransform proportionalDisplay = null;
	[SerializeField] Text percentageText = null;

	int temporaryTime;

	private bool On { get; set; }
	public int Index { set; get; } = -1;
	public int Time { set; get; } = 20;


	const int TIME_INCREMENT = 5;

	public delegate void TimeChanged (int index, int time);
	public static event TimeChanged onTimeChanged;


	void Awake ()
	{
		temporaryTime = Time;
	}

	// Button
	public void ChangeTime (bool increase)
	{
		int x = increase ? TIME_INCREMENT : -TIME_INCREMENT;
		int new_time = Mathf.Clamp(x + Time, 0, 90);
		UpdateTime (new_time);
	}

	// Button
	public void SetCoverActive (bool active)
	{
		On = !active;

		if (cover) { cover.SetActive (active); }

		if (On)
		{
			UpdateTime (temporaryTime);
		}
		else
		{
			temporaryTime = Time;
			UpdateTime (0);
		}
	}

	void UpdateTime (int newTime)
	{
		if (Index != -1)
		{
			Time = newTime;

			if (onTimeChanged != null)
			{
				onTimeChanged (Index, Time);
			}
		}
	}

	public void SetTime (int newTime)
	{
		Time = newTime;
	}

	public void ShowValue ()
	{
		localDisplay.text = Time.ToString ();

		if (Time != 0)
		{
			float percent = Mathf.Round(100f * (float)Time / (float)Timeline.GetTotalTime());
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