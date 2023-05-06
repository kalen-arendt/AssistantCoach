using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

public class SelfDestruct : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private float time;
	[SerializeField] private bool destroyOnClick;


	// Use this for initialization
	private void Start()
	{
		StartCoroutine(SelfDestroy());
	}


	private IEnumerator SelfDestroy()
	{
		yield return new WaitForSecondsRealtime(time);
		Destroy(gameObject);
	}


	public void OnPointerClick(PointerEventData data)
	{
		if (destroyOnClick)
		{
			StopAllCoroutines();
			Destroy(gameObject);
		}
	}
}
