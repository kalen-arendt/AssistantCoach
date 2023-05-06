using System.Collections.Generic;
using UnityEngine;


public class CurrentOutputController : BaseOutputSessionManager
{
	[SerializeField] Message messageWindow = null;

	AgeDateManager ageDateManager;
	Timeline timeline;
	CurriculumSelection curriculumSelection;
	SavedSessionPreviewManager savedSessionManager;

	List<CurrentDrillDisplay> currentSessionOutputs = new List<CurrentDrillDisplay> ();
	List<CurriculumItem> items = new List<CurriculumItem> ();

	const string MSG_SESSION_NOT_FULL = "Fill your session to advance";
	const string MSG_SESSION_SAVED = "Session Saved";


	void Start ()
	{
		ageDateManager = FindObjectOfType<AgeDateManager> ();
		timeline = FindObjectOfType<Timeline> ();
		curriculumSelection = FindObjectOfType<CurriculumSelection>();
		savedSessionManager = FindObjectOfType<SavedSessionPreviewManager> ();
	}


	public void CreateTrainingSession (List<CurriculumItem> topics)
	{
		if (topics.Count != timeline.Blocks)
		{
			Instantiate (messageWindow).ShowMessage (MSG_SESSION_NOT_FULL);;
			return;
		}

		if (topics == items)
		{
			appController.CallNextA (GetComponent<Canvas>());
			return;
		}


		int age = ageDateManager.Age;
		items = new List<CurriculumItem> (topics);

		ShowSessionData (
			ageDateManager.Date,
			curriculumSelection.Summary,
			"U" + age,
			Timeline.TotalTime
		);

		int i = 0;

		while (topics.Count < currentSessionOutputs.Count)
		{
			var v = currentSessionOutputs [currentSessionOutputs.Count - 1];
			currentSessionOutputs.RemoveAt (currentSessionOutputs.Count - 1);
			Destroy (v.gameObject);
		}

		for (;i < topics.Count; i++)
		{
			if (i >= currentSessionOutputs.Count)
			{
				GameObject obj = Instantiate (template, parent) as GameObject;
				var drill = obj.GetComponent<CurrentDrillDisplay> ();
				currentSessionOutputs.Add(drill);
			}

			var currentOutput = currentSessionOutputs [i];

			currentOutput.SetPossibleDrills (
				sessionDataBase.GetPossibleDrillsList (
					topics [i].topic,
					drillType.Any,
					age
				)
			);

			currentOutput.SetDetails (i, Timeline.timeList [i], topics [i].topic);
			currentOutput.ShowRecentDrill ();
		}


		SetAllLayouts ();


		appController.CallNextA (GetComponent<Canvas>());
	}


	public void SaveCurrentSession ()
	{
		List<SubjectData> sessionData = new List<SubjectData> ();

		foreach (CurrentDrillDisplay drill in currentSessionOutputs)
		{
			sessionData.Add (drill.GetSubjectData);
		}

		sessionData.Reverse ();

		SaveObject saveObj = new SaveObject (
			sessionData.ToArray (),
			ageDateManager.Date,
			ageDateManager.Age,
			Timeline.TotalTime
		);

		PlayerPrefsManager.SavedSessions.AddSession (saveObj);
		savedSessionManager.UpdateSavedSessions ();
		Instantiate (messageWindow).ShowMessage(MSG_SESSION_SAVED);

	}


	public void RefreshAll ()
	{
		foreach (CurrentDrillDisplay display in currentSessionOutputs)
		{
			display.ShowRandomDrill ();
		}
	}
}