using System;
using UnityEngine;
using UnityEngine.UI;

public class ChildContentFitter : ContentSizeFitter
{
	//Events
	public event Action<Vector2> OnDimentionsChangedEvent;


	//Fields
	private RectTransform rectTransform;
	private Vector2 latestSize;


	//Inherited Methods

	protected override void OnEnable ()
	{
		base.OnEnable ();

		rectTransform = (RectTransform)transform;
		latestSize = rectTransform.sizeDelta;
	}


	void LateUpdate ()
	{
		if (CheckDifference (rectTransform.sizeDelta)) {
			SendSizeDelta ();
		}
	}



	//Methods

	bool CheckDifference (Vector2 size)
	{
		if (size == latestSize) {
			return true;
		}

		latestSize = size;
		return false;
	}


	void SendSizeDelta ()
	{
		if (rectTransform == null) {
			rectTransform = (RectTransform)transform;
		}

		Vector2 sizeDelta = rectTransform.sizeDelta;

		if (OnDimentionsChangedEvent != null) {
			OnDimentionsChangedEvent (sizeDelta);
		}
		else {
			GetComponentInParent<ParentContentFitter> ().ChangeDimentions (sizeDelta);
		}
	}
}