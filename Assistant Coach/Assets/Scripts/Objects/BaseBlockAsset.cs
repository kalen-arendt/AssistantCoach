using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBlockAsset : ScriptableObject
{

	//Fields

	public Sprite drillImage;

	[Range (9, 17)] public int minAge;
	[Range (10,18)] public int maxAge;

	public drillType type;

	public List<Subjects> MajorTopics;
	public List<Subjects> MinorTopics;



	//Abstract Properties

	public abstract BlockOutputData blockStruct {get;}



	//Methods

	void OnEnable ()
	{
		if (maxAge < 10) maxAge = 10;
		if (minAge < 9) minAge = 9;
	}
}