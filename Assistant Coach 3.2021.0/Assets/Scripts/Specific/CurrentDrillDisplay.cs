using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrentDrillDisplay : BaseDrillDisplay
{
	[SerializeField] SessionDataBase sessionDataBase = null;

	int currentIndex = -1;
	int drillDisplayIndex = 0;
	int duration = 0;

	List<int> recents = new List<int> ();
	List<BaseBlockAsset> possibleDrills = null;


	public int GetIndexOfCurrent {
		get {
			if (possibleDrills[currentIndex] != null)
			{
				var index = sessionDataBase.Sessions.IndexOf(possibleDrills [currentIndex]);

				for (int i = 0; i < sessionDataBase.Sessions.Count; i++)
				{
					if (sessionDataBase.Sessions [i] == possibleDrills [currentIndex])
					{
						Debug.LogFormat("index = {0}, i = {1}", index, i); 
						return i;
					}
				}
			}		
			return -1;
		}
	}

	public SubjectData GetSubjectData {
		get { return new SubjectData (GetIndexOfCurrent, duration, subject); }
	}


	public void SetDetails (int index, int time, BlockTopic topic)
	{
		this.drillDisplayIndex = index;
		this.duration = time;
		this.subject = topic;
	}

	public void SetPossibleDrills (List<BaseBlockAsset> drills)
	{
		possibleDrills = drills;
	}

	public void ShowRecentDrill ()
	{
		if (currentIndex != -1) {
			ShowDrill (currentIndex);
		} else {
			ShowRandomDrill ();
		}
	}

	public void ShowRandomDrill ()
	{
		int x = Random.Range (0, possibleDrills.Count);

		while (x == currentIndex && !IsNewDrill (x))
		{
			x = Random.Range (0, possibleDrills.Count);
		}

		ShowDrill (x); 
	}

	bool IsNewDrill (int x)
	{
		if (ItarateThroughDrills (x))
			return true;

		if (recents.Count == possibleDrills.Count)
			recents.Clear ();

		return ItarateThroughDrills (x);
	}


	bool ItarateThroughDrills (int index)
	{
		//if the index is the same as the previous ones break;
		for (int i = 0; i < recents.Count; i++)
			if (index == recents [i])
				return false;
			else
				continue;

		recents.Add (index);
		return true;
	}


	void ShowDrill (int index)
	{
		if (index >= possibleDrills.Count) { return; }

		currentIndex = index;
		BlockOutputData block = possibleDrills [index].blockStruct;

		ShowDrillBaseRootIndex (block, drillDisplayIndex, duration, subject);
	}
}