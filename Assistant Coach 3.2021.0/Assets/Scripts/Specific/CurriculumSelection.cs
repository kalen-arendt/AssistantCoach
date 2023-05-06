using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class CurriculumSelection : MonoBehaviour
{
	[SerializeField] private Transform parent = null;
	[SerializeField] private GameObject summaryTemplate = null;
	[SerializeField] private CurrentOutputController outputController = null;

	private List<CurriculumItem> CurrentPracticeStructure { get; } = new();
	private List<GameObject> SummaryObjList { get; } = new();

	public string GetSummary()
	{
		return CurrentPracticeStructure.Select(x => x.topic).Summarize();
	}


	public void AddEvent(CurriculumItem item)
	{
		Timeline timeline = FindObjectOfType<Timeline>();

		if (timeline == null )
		{
			Debug.LogWarning($"Object of type `{nameof(Timeline)}` not found.");
			return;
			//throw new MissingReferenceException();
		}


		if (CurrentPracticeStructure.Count >= timeline.SelectionCount())
		{
			return;
		}

		CurrentPracticeStructure.Add(item);

		GameObject obj = Instantiate (summaryTemplate, parent);
		SummaryObjList.Add(obj);

		Text text = obj.GetComponentInChildren<Text> ();
		text.color = item.color;
		text.text = item.topic.AsString();
	}

	public void RemoveBlock(GameObject Obj)
	{
		var index = SummaryObjList.IndexOf (Obj);

		if (index < CurrentPracticeStructure.Count)
		{
			CurrentPracticeStructure.RemoveAt(index);
		}

		if (index < SummaryObjList.Count)
		{
			SummaryObjList.RemoveAt(index);
		}
	}

	public void SetBlockLimit(int max)
	{
		while (CurrentPracticeStructure.Count > max)
		{
			SummaryObjList[^1].GetComponent<DisplayObj>().RemoveBlock();
		}
	}

	public void CreateSession()
	{
		outputController.CreateTrainingSession(CurrentPracticeStructure);
	}
}
