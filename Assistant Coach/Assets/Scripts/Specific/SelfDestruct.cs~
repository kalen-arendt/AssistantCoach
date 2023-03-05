using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelfDestruct : MonoBehaviour, IPointerClickHandler {


	public float time;
	public bool destroyOnClick;


	// Use this for initialization
	void Start ()
	{
		StartCoroutine (SelfDestroy ());
	}


	private IEnumerator SelfDestroy ()
	{
		yield return new WaitForSecondsRealtime (time);
		Destroy (gameObject);
	}


	public void OnPointerClick (PointerEventData data)
	{
		if (destroyOnClick)
		{
			StopAllCoroutines ();
			Destroy (gameObject);
		}
	}



}
