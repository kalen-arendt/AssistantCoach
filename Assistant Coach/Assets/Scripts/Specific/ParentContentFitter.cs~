using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentContentFitter : MonoBehaviour {


	private ChildContentFitter childContentFitter;
	private RectTransform rectTransform;

	void OnEnable ()
	{
		childContentFitter = GetComponentInChildren<ChildContentFitter> ();
		childContentFitter.OnDimentionsChangedEvent += ChangeDimentions;

		rectTransform = (RectTransform)GetComponent<Transform> ();
	}

	int c = 0;
	void LateUpdate ()
	{
		c++;
		if (c >= 30)
		{
			var layouts = GetComponentsInParent<LayoutGroup> ();
			var layoutList = new List<LayoutGroup> ();

			foreach (LayoutGroup lg in layouts)
				layoutList.Add (lg);

			layoutList.Reverse ();


			foreach (LayoutGroup lg in layoutList)
			{
				lg.CalculateLayoutInputHorizontal ();
				lg.SetLayoutVertical ();
			}
		}
	}


	public void ChangeDimentions (Vector2 SizeDelta)
	{
		if (rectTransform == null)
			rectTransform = (RectTransform)GetComponent<Transform> ();

		if (rectTransform.pivot.y != 1)
			rectTransform.pivot = new Vector2 (0.5f, 1);


		if (SizeDelta.y == rectTransform.rect.y)
			return;

		SizeDelta.x = rectTransform.sizeDelta.x;
		rectTransform.sizeDelta = SizeDelta;
	}

}