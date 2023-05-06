using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timeline : MonoBehaviour 
{
	[SerializeField] List<TimeSelect> timers = new List<TimeSelect> ();
	[SerializeField] Text totalTime = null;
	[SerializeField] Text sessionLength = null;

	CurriculumSelection curriculum;

	const int MAX_BLOCKS = 4;

	public static List<int> timeList = new List<int> (4) {0,0,0,0};

	public static int TotalTime { get {
			int total = 0;
			foreach (int t in timeList) total += t;
			return total;
		}
	}

	public int Blocks { get {
			int i = 0;
			foreach (int time in timeList) if (time > 0) i++;
			return i;
		}
	}

	void Awake ()
	{
		for (int i = 0; i < timers.Count; i++) {
			timers [i].Index = i;
		}
	}

	void Start ()
	{
		curriculum = FindObjectOfType<CurriculumSelection>();
		GetSavedTimes ();
		UpdateTimers();

		TimeSelect.onTimeChanged += OnTimeChanged;
	}

	void OnTimeChanged (int timerIndex, int time)
	{
		timeList [timerIndex] = time;
		UpdateTimers ();
	}

	void UpdateTimers ()
	{
		float min_anchor = 0;
		float max_anchor = 0;

		for (int i = 0; i < timers.Count; i++, min_anchor = max_anchor)
		{
			var timer = timers[i];
			var time = timeList[i];

			if (timeList [i] != 0)
			{
				max_anchor += (float)timeList [i] / (float)TotalTime;
			}
			else if (TotalTime == 0)
			{
				max_anchor += 0.25f;
			}

			timer.SetTime(time);
			timer.ShowValue();
			timer.ResizeToPercentage(min_anchor, max_anchor);
		}

		totalTime.text = TotalTime.ToString ();
		sessionLength.text = Blocks.ToString ();
		curriculum.SetBlockLimit (Blocks);
	}

	void GetSavedTimes ()
	{
		timeList = PlayerPrefsManager.Settings.Timeline;

		for (int i = 0; i < 4; i++)
		{
			bool startActive = timeList [i] == 0;
			timers[i].SetCoverActive (startActive);

			if (startActive)
			{
				timers[i].SetTime (timeList [i]);
			}
		}
	}

	public void SetSavedTimes ()
	{
		PlayerPrefsManager.Settings.Timeline = timeList;
	}
}