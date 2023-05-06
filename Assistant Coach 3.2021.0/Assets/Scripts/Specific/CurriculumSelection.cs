using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class CurriculumSelection : MonoBehaviour
{
	[SerializeField] Transform parent = null;
	[SerializeField] GameObject summaryTemplate = null;
	[SerializeField] CurrentOutputController outputController = null;

	readonly List<GameObject> summaryObjList = new();
	readonly List<CurriculumItem> currentPracticeStructure = new();

	public string GetSummary()
	{
		return string.Join("-", currentPracticeStructure.Select(x => SubjectAsString(x.topic)));
	}


	public void AddEvent (CurriculumItem item)
	{
		var timeline = FindObjectOfType<Timeline>();

		if (currentPracticeStructure.Count >= timeline.CheckNull()?.GetBlocks())
		{
			return;
		}

		currentPracticeStructure.Add (item);

		GameObject obj = Instantiate (summaryTemplate, parent);
		summaryObjList.Add (obj);

		Text text = obj.GetComponentInChildren<Text> ();
		text.color = item.color;
		text.text = SubjectAsString(item.topic);
	}

	public void RemoveBlock (GameObject Obj)
	{
		int index = summaryObjList.IndexOf (Obj);

		if (index < currentPracticeStructure.Count)
		{
			currentPracticeStructure.RemoveAt (index);
		}

		if (index < summaryObjList.Count)
		{
			summaryObjList.RemoveAt (index);
		}
	}

	public void SetBlockLimit (int max)
	{
		while (currentPracticeStructure.Count > max)
		{
			summaryObjList [^1].GetComponent<DisplayObj> ().RemoveBlock ();
		}
	}

	public void CreateSession ()
	{
		outputController.CreateTrainingSession (currentPracticeStructure);
	}

	public static string SubjectAsString (BlockTopic subject)
	{
		return subject switch
		{
			BlockTopic.PR		=> "P+R",
			BlockTopic.PwG		=> "Pw/G",
			BlockTopic.PwT		=> "Pw/T",
			BlockTopic.VA		=> "V+A",
			BlockTopic._1v1D	=> "1v1D",
			_					=> subject.ToString(),
		};
	}
}
