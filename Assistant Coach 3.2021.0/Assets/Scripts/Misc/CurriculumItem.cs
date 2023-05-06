using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CurriculumItem 
{
	readonly public BlockTopic topic;
	readonly public Color color;



	public CurriculumItem (BlockTopic _topic, Color _color)
	{
		topic = _topic;
		color = _color;
	}
}
