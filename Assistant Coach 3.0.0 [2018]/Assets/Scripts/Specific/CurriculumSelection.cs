using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurriculumSelection : MonoBehaviour
{
	[SerializeField] Transform parent = null;
	[SerializeField] GameObject summaryTemplate = null;
	[SerializeField] CurrentOutputController outputController = null;

	List<GameObject> summaryObjList = new List<GameObject>();
	List<CurriculumItem> currentPracticeStructure = new List<CurriculumItem> (); 

	public string Summary { get {
			string str = "";
			foreach (CurriculumItem item in currentPracticeStructure) {
				str += (SubjectAsString(item.topic) + "-");
			}
			str = str.Remove (str.Length - 1);
			return str;
		}
	}


	public void AddEvent (CurriculumItem item)
	{
		if (currentPracticeStructure.Count >= FindObjectOfType<Timeline>()?.Blocks) { return; }

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
			summaryObjList [summaryObjList.Count - 1].GetComponent<DisplayObj> ().RemoveBlock ();
		}
	}

	public void CreateSession ()
	{
		outputController.CreateTrainingSession (currentPracticeStructure);
	}

	public static string SubjectAsString (Subjects subject)
	{
		switch (subject) {
			case Subjects.PR:		return "P+R";
			case Subjects.PwG:		return "Pw/G";
			case Subjects.PwT:		return "Pw/T";
			case Subjects.VA: 		return "V+A";
			case Subjects._1v1D:	return "1v1D";
			default : return subject.ToString ();
		}
	}
}

public enum Subjects {SA, PL, BS, D, R, PR, SSG, PwT, PwG, VA, H, _1v1D}
public enum drillType {Activity, Game, Any}