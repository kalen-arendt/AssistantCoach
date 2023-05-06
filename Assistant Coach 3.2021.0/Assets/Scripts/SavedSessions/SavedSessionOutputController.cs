using UnityEngine;


public class SavedSessionOutputController : BaseOutputSessionManager
{
	public void ShowSavedSession(int index)
	{
		SaveObject session = PlayerPrefsManager.SavedSessions.GetSavedSession (index);
		SubjectData[] subjects = session.SubjectDataArray;

		ShowSessionData(session.Date, session.GetTopicSummary(), session.GetAge(), session.TotalTime);

		RemoveExcessTemplates(subjects.Length);

		//Instantiates necessary Prefabs
		for (var subject_index = 0; subject_index < subjects.Length; subject_index++)
		{
			if (subject_index >= outputs.Count)
			{
				GameObject obj = Instantiate (template, parent);
				outputs.Add(obj.GetComponent<BaseDrillDisplay>());
			}

			SubjectData subject = subjects [subject_index];

			outputs[subject_index].ShowDrillBaseRootIndex(
				sessionDataBase.Sessions[subject.dataBaseIndex].blockStruct,
				subject_index,
				subject.time,
				subject.subject
			);
		}

		SetAllLayouts();

		Canvas myCanvas = GetComponent<Canvas> ();
		appController.CallPreviousA(myCanvas);
	}

	private void RemoveExcessTemplates(int templatesRequired)
	{
		while (outputs.Count > templatesRequired)
		{
			var lastIndex = outputs.Count - 1;
			Destroy(outputs[lastIndex].gameObject);
			outputs.RemoveAt(lastIndex);
		}
	}
}