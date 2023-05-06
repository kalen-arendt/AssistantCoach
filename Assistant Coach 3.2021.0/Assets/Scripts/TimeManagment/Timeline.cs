using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class Timeline : MonoBehaviour
{
	[SerializeField] private List<TimeSelect> timers = new();
	[SerializeField] private Text totalTime = null;
	[SerializeField] private Text sessionLength = null;
	private CurriculumSelection curriculum;
	private const int MAX_BLOCKS = 4;

	public static List<int> timeList = new(4) {0,0,0,0};

	public static int GetTotalTime()
	{
		var total = 0;
		foreach (var t in timeList)
		{
			total += t;
		}

		return total;
	}
	public int GetBlocks()
	{
		var i = 0;
		foreach (var time in timeList)
		{
			if (time > 0)
			{
				i++;
			}
		}

		return i;
	}

	private void Awake()
	{
		for (var i = 0; i < timers.Count; i++)
		{
			timers[i].Index = i;
		}
	}

	private void Start()
	{
		curriculum = FindObjectOfType<CurriculumSelection>();
		GetSavedTimes();
		UpdateTimers();

		TimeSelect.onTimeChanged += OnTimeChanged;
	}

	private void OnTimeChanged(int timerIndex, int time)
	{
		timeList[timerIndex] = time;
		UpdateTimers();
	}

	private void UpdateTimers()
	{
		float min_anchor = 0;
		float max_anchor = 0;

		for (var i = 0; i < timers.Count; i++, min_anchor = max_anchor)
		{
			TimeSelect timer = timers[i];
			var time = timeList[i];

			if (timeList[i] != 0)
			{
				max_anchor += timeList[i] / (float)GetTotalTime();
			}
			else if (GetTotalTime() == 0)
			{
				max_anchor += 0.25f;
			}

			timer.SetTime(time);
			timer.ShowValue();
			timer.ResizeToPercentage(min_anchor, max_anchor);
		}

		totalTime.text = GetTotalTime().ToString();
		sessionLength.text = GetBlocks().ToString();
		curriculum.SetBlockLimit(GetBlocks());
	}

	private void GetSavedTimes()
	{
		timeList = PlayerPrefsManager.Settings.Timeline;

		for (var i = 0; i < 4; i++)
		{
			var startActive = timeList [i] == 0;
			timers[i].SetCoverActive(startActive);

			if (startActive)
			{
				timers[i].SetTime(timeList[i]);
			}
		}
	}

	public void SetSavedTimes()
	{
		PlayerPrefsManager.Settings.Timeline = timeList;
	}
}