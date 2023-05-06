using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "DataBase/Drill Collection")]
public class SessionDataBase : ScriptableObject
{
	[SerializeField] List<BaseBlockAsset> sessions = new List<BaseBlockAsset> ();

	public List<BaseBlockAsset> Sessions { get { return sessions; } }


	public BaseBlockAsset GetRandomDrill ()
	{
		int x = 0, y = sessions.Count;
		var a = Random.Range(x,y);

		return sessions [a];
	}


	public List<BaseBlockAsset> GetPossibleDrillsList (BlockTopic topic, BlockType type, int Age)
	{
		List<BaseBlockAsset> list = new List<BaseBlockAsset> ();

		foreach (BaseBlockAsset drill in sessions)
			if (drill.MajorTopics.Contains (topic) && drill.minAge <= Age && drill.maxAge >= Age)
			if ((type == BlockType.Activity && drill.type == BlockType.Activity) ||
				(type == BlockType.Game && drill.type == BlockType.Game) ||
				type == BlockType.Any)
					list.Add (drill);

		return list;
	}
}