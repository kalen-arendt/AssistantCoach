using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildContentFitter : ContentSizeFitter {

	//Events

	public delegate void OnDimentionsChangedDelagate (Vector2 vector2);
	public event OnDimentionsChangedDelagate OnDimentionsChangedEvent;



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
		if (CheckDifference (rectTransform.sizeDelta))
			SendSizeDelta ();
	}



	//Methods

	bool CheckDifference (Vector2 size)
	{
		if (size == latestSize)
			return true;

		latestSize = size;
		return false;
	}


	void SendSizeDelta ()
	{
		if (rectTransform == null)
			rectTransform = (RectTransform)transform;
		
		var sizeDelta = rectTransform.sizeDelta;

		if (OnDimentionsChangedEvent != null)
			OnDimentionsChangedEvent (sizeDelta);
		else
			GetComponentInParent<ParentContentFitter> ().ChangeDimentions (sizeDelta);
	}

}