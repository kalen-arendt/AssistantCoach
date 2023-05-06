using UnityEngine;

public readonly struct CurriculumItem
{
	readonly public BlockTopic topic;
	readonly public Color color;



	public CurriculumItem(BlockTopic _topic, Color _color)
	{
		topic = _topic;
		color = _color;
	}
}
