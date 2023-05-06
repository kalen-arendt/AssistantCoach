using UnityEngine;
using UnityEngine.UI;

public class TimeSelect : MonoBehaviour
{
	[SerializeField] private Text localDisplay = null;
	[SerializeField] private GameObject cover = null;
	[SerializeField] private RectTransform proportionalDisplay = null;
	[SerializeField] private Text percentageText = null;
	private int temporaryTime;

	private bool On { get; set; }
	public int Index { set; get; } = -1;
	public int Time { set; get; } = 20;

	private const int TIME_INCREMENT = 5;

	public delegate void TimeChanged(int index, int time);
	public static event TimeChanged onTimeChanged;

	private void Awake()
	{
		temporaryTime = Time;
	}

	// Button
	public void ChangeTime(bool increase)
	{
		var x = increase ? TIME_INCREMENT : -TIME_INCREMENT;
		var new_time = Mathf.Clamp(x + Time, 0, 90);
		UpdateTime(new_time);
	}

	// Button
	public void SetCoverActive(bool active)
	{
		On = !active;

		if (cover)
		{ cover.SetActive(active); }

		if (On)
		{
			UpdateTime(temporaryTime);
		}
		else
		{
			temporaryTime = Time;
			UpdateTime(0);
		}
	}

	private void UpdateTime(int newTime)
	{
		if (Index != -1)
		{
			Time = newTime;

			onTimeChanged?.Invoke(Index, Time);
		}
	}

	public void SetTime(int newTime)
	{
		Time = newTime;
	}

	public void ShowValue()
	{
		localDisplay.text = Time.ToString();

		if (Time != 0)
		{
			var percent = Mathf.Round(100f * Time / Timeline.GetTotalTime());
			percentageText.text = percent + "%";
		}
		else
		{
			percentageText.text = "";
		}
	}

	public void ResizeToPercentage(float min, float max)
	{
		proportionalDisplay.anchorMin = new Vector2(min, 0);
		proportionalDisplay.anchorMax = new Vector2(max, 1);
	}
}