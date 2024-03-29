﻿using UnityEngine;

public struct BlockOutputData
{
	public readonly Sprite mySprite;
	public readonly string descriptionText;

	public BlockOutputData (Sprite sprite, string description) 
	{
		mySprite = sprite;
		descriptionText = description;
	}
}