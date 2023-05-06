using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;


public class Timeline : MonoBehaviour
{
	[SerializeField] private List<TimeSelect> timers = new();
	[SerializeField] private Text totalTime = null;
	[SerializeField] private Text sessionLength = null;
	private CurriculumSelection curriculum;

	public static List<int> timeList = new(MAX_BLOCKS) {0,0,0,0};

	private const int MAX_BLOCKS = 4;


	public static int GetTotalTime()
	{
		return timeList.Aggregate(
			seed: 0,
			func: (total, next) => total + next
		);
	}
	public int SelectionCount()
	{
		return timeList.Count(time => time > 0);
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
		sessionLength.text = SelectionCount().ToString();
		curriculum.SetBlockLimit(SelectionCount());
	}

	private void GetSavedTimes()
	{
		timeList = PlayerPrefsManager.Settings.Timeline;

		for (var i = 0; i < MAX_BLOCKS ; i++)
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