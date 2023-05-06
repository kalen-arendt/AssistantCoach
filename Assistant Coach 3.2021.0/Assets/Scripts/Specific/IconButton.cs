using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent (typeof (Image))]
public class IconButton : UIBehaviour, IPointerClickHandler
{
	[SerializeField] Color color = Color.white;
	[SerializeField] BlockTopic topic = BlockTopic.PL;

	CurriculumItem CurriculumItem => new(topic, color);

	//Interfaces

	public void OnPointerClick (PointerEventData data)
	{
		SelectTopic();
	}

	public void SelectTopic()
	{
		var curriculum = FindObjectOfType<CurriculumSelection>();
		if (curriculum != null)
		{
			curriculum.AddEvent(CurriculumItem);
		}
	}
}


