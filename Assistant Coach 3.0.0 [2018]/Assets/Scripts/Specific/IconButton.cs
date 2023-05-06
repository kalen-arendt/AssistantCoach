using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent (typeof (Image))]
public class IconButton : UIBehaviour, IPointerClickHandler
{
	[SerializeField] Color color = Color.white;
	[SerializeField] Subjects topic = Subjects.PL;

	CurriculumItem curriculumItem { get { return new CurriculumItem (topic, color); }}

	//Interfaces

	public void OnPointerClick (PointerEventData data)
	{
		var curriculum = FindObjectOfType<CurriculumSelection> ();
		curriculum.AddEvent (curriculumItem);
	}
}


