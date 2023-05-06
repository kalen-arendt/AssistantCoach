using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconButton : UIBehaviour, IPointerClickHandler
{
	[SerializeField] private Color color = Color.white;
	[SerializeField] private BlockTopic topic = BlockTopic.PL;

	private CurriculumItem CurriculumItem => new(topic, color);

	//Interfaces

	public void OnPointerClick(PointerEventData data)
	{
		SelectTopic();
	}

	public void SelectTopic()
	{
		CurriculumSelection curriculum = FindObjectOfType<CurriculumSelection>();
		if (curriculum != null)
		{
			curriculum.AddEvent(CurriculumItem);
		}
	}
}


