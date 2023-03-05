using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent (typeof (GraphicRaycaster))]
public class UserConfirmation : MonoBehaviour
{
	[SerializeField] Text Summary = null;
	[SerializeField] Button confirm = null;
	[SerializeField] Button reject = null;

	void OnEnable ()
	{
		confirm.onClick.AddListener (() => Destroy(gameObject));
		reject.onClick.AddListener (() => Destroy(gameObject));
	}

	public void SetButtons (UnityAction onConfirmationFunction, string summary)
	{
		confirm.onClick.AddListener (onConfirmationFunction);
		Summary.text = summary;
	}
}