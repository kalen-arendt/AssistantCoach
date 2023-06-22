using System.Collections.Generic;

using UnityEngine;


public class CurrentDrillDisplay : BaseDrillDisplay
{
	[SerializeField] private SessionDataBase sessionDataBase = null;

	private int CurrentIndex { get; set; } = -1;
	private int DrillDisplayIndex { get; set; } = 0;
	private int Duration { get; set; } = 0;
	public List<int> Recents { get; } = new();
	public List<BaseBlockAsset> PossibleDrills { get; set; } = null;

	public SubjectData GetSubjectData => new(GetIndexOfCurrent(), Duration, Subject);

	private int GetIndexOfCurrent()
	{
		if (PossibleDrills[CurrentIndex] != null)
		{
			var index = sessionDataBase.Sessions.IndexOf(PossibleDrills [CurrentIndex]);

			for (var i = 0; i < sessionDataBase.Sessions.Count; i++)
			{
				if (sessionDataBase.Sessions[i] == PossibleDrills[CurrentIndex])
				{
					Debug.LogFormat("index = {0}, i = {1}", index, i);
					return i;
				}
			}
		}

		return -1;
	}



	public void SetDetails(int index, int time, BlockTopic topic)
	{
		DrillDisplayIndex = index;
		Duration = time;
		Subject = topic;
	}

	public void SetPossibleDrills(List<BaseBlockAsset> drills)
	{
		PossibleDrills = drills;
	}

	public void ShowRecentDrill()
	{
		// TODO: fixme
		//(CurrentIndex != -1).IfThenElse(
		//	() => ShowDrill(CurrentIndex),
		//	() => ShowRandomDrill()
		//);
	}

	public void ShowRandomDrill()
	{
		var x = Random.Range (0, PossibleDrills.Count);

		while (x == CurrentIndex && !IsNewDrill(x))
		{
			x = Random.Range(0, PossibleDrills.Count);
		}

		ShowDrill(x);
	}

	private bool IsNewDrill(int x)
	{
		if (ItarateThroughDrills(x))
		{
			return true;
		}

		if (Recents.Count == PossibleDrills.Count)
		{
			Recents.Clear();
		}

		return ItarateThroughDrills(x);
	}

	private bool ItarateThroughDrills(int index)
	{
		//if the index is the same as the previous ones break;
		for (var i = 0; i < Recents.Count; i++)
		{
			if (index == Recents[i])
			{
				return false;
			}
		}

		Recents.Add(index);
		return true;
	}

	private void ShowDrill(int index)
	{
		if (index >= PossibleDrills.Count)
		{ return; }

		CurrentIndex = index;
		BlockOutputData block = PossibleDrills [index].blockStruct;

		ShowDrillBaseRootIndex(block, DrillDisplayIndex, Duration, Subject);
	}
}