using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class CurrentOutputController : BaseOutputSessionManager
{
	[SerializeField] private Message messageWindow = null;
	
	private AgeDateManager ageDateManager;
	private Timeline timeline;
	private CurriculumSelection curriculumSelection;
	private SavedSessionPreviewManager savedSessionManager;
	private readonly List<CurrentDrillDisplay> currentSessionOutputs = new();
	private List<CurriculumItem> items = new();

	private const string MSG_SESSION_NOT_FULL = "Fill your session to advance";
	private const string MSG_SESSION_SAVED = "Session Saved";

	private void Start()
	{
		ageDateManager = FindObjectOfType<AgeDateManager>();
		timeline = FindObjectOfType<Timeline>();
		curriculumSelection = FindObjectOfType<CurriculumSelection>();
		savedSessionManager = FindObjectOfType<SavedSessionPreviewManager>();
	}


	public void CreateTrainingSession(List<CurriculumItem> topics)
	{
		if (topics.Count != timeline.SelectionCount())
		{
			Instantiate(messageWindow).ShowMessage(MSG_SESSION_NOT_FULL);
			return;
		}

		if (topics == items)
		{
			appController.CallNextA(GetComponent<Canvas>());
			return;
		}


		var age = ageDateManager.Age;
		items = new List<CurriculumItem>(topics);

		ShowSessionData(
			ageDateManager.Date,
			curriculumSelection.GetSummary(),
			"U" + age,
			Timeline.GetTotalTime()
		);

		var i = 0;

		while (topics.Count < currentSessionOutputs.Count)
		{
			CurrentDrillDisplay v = currentSessionOutputs [^1];
			currentSessionOutputs.RemoveAt(currentSessionOutputs.Count - 1);
			Destroy(v.gameObject);
		}

		for (; i < topics.Count; i++)
		{
			if (i >= currentSessionOutputs.Count)
			{
				GameObject obj = Instantiate (template, parent);
				CurrentDrillDisplay drill = obj.GetComponent<CurrentDrillDisplay> ();
				currentSessionOutputs.Add(drill);
			}

			CurrentDrillDisplay currentOutput = currentSessionOutputs [i];

			currentOutput.SetPossibleDrills(
				sessionDataBase.GetPossibleDrillsList(
					topics[i].topic,
					BlockType.Any,
					age
				)
			);

			currentOutput.SetDetails(i, Timeline.blockDurationList[i], topics[i].topic);
			currentOutput.ShowRecentDrill();
		}


		SetAllLayouts();


		appController.CallNextA(GetComponent<Canvas>());
	}


	public void SaveCurrentSession()
	{
		var sessionData = new List<SubjectData> ();

		foreach (CurrentDrillDisplay drill in currentSessionOutputs)
		{
			sessionData.Add(drill.GetSubjectData);
		}

		sessionData.Reverse();

		var saveObj = new SaveObject (
			sessionData.ToArray (),
			ageDateManager.Date,
			ageDateManager.Age,
			Timeline.GetTotalTime()
		);

		PlayerPrefsManager.SavedSessions.AddSession(saveObj);
		savedSessionManager.UpdateSavedSessions();
		Instantiate(messageWindow).ShowMessage(MSG_SESSION_SAVED);

	}


	public void RefreshAll()
	{
		foreach (CurrentDrillDisplay display in currentSessionOutputs)
		{
			display.ShowRandomDrill();
		}
	}
}