using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseOutputSessionManager : MonoBehaviour
{
	[SerializeField] protected SessionDataBase sessionDataBase = null;
	[SerializeField] protected Transform parent = null;
	[SerializeField] protected GameObject template = null;

	[SerializeField] Text ageText = null;
	[SerializeField] Text summary = null;
	[SerializeField] Text date = null;
	[SerializeField] Text time = null;
	[SerializeField] Button layoutToggle = null;

	public SessionDataBase SessionDataBase { get { return sessionDataBase; } }

	protected ApplicationController appController;
	protected List<BaseDrillDisplay> outputs = new List<BaseDrillDisplay> ();


	static bool portrait = false;


	void OnEnable ()
	{
		appController = FindObjectOfType<ApplicationController> ();
		ToggleSprite ();
	}

	public void ShowSessionData (Date Date, string Summary, string Age, int Time)
	{
		date.text = Date.LongDate ();
		summary.text = Summary;
		ageText.text = Age;
		time.text = Time + "min";
	}

	public void ToggleLayouts ()
	{
		portrait = !portrait;

		foreach (var item in outputs)
		{
			item.ToggleLayoutType ();
		}

		ToggleSprite ();
	}


	void ToggleSprite ()
	{
		Vector3 angles = layoutToggle.transform.localEulerAngles;
		angles.z = portrait ? 90 : 0;
		layoutToggle.transform.localEulerAngles = angles;
	}

	protected void SetAllLayouts ()
	{
		foreach (var item in outputs)
		{
			if (item.Portrait != portrait)
			{
				item.ToggleLayoutType ();
			}
		}
	}
}