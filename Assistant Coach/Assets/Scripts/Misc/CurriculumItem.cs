using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CurriculumItem 
{
	readonly public Subjects topic;
	readonly public Color color;



	public CurriculumItem (Subjects _topic, Color _color)
	{
		topic = _topic;
		color = _color;
	}
}
