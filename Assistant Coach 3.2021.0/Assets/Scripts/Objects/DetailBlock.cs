﻿using UnityEngine;

[CreateAssetMenu(menuName = "TrainingBlock/Detail Block", fileName = "DetailedBlock", order = 2)]
public class DetailBlock : BaseBlockAsset
{

	//Fields

	[TextArea(25,40)] public string description;



	//Properties

	public override BlockOutputData blockStruct => new(drillImage, description);
}