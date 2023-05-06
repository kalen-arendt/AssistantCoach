using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BaseOutputSessionManager : MonoBehaviour
{
	[SerializeField] protected SessionDataBase sessionDataBase = null;
	[SerializeField] protected Transform parent = null;
	[SerializeField] protected GameObject template = null;

	[SerializeField] private Text ageText = null;
	[SerializeField] private Text summary = null;
	[SerializeField] private Text date = null;
	[SerializeField] private Text time = null;
	[SerializeField] private Button layoutToggle = null;

	public SessionDataBase SessionDataBase => sessionDataBase;

	protected ApplicationController appController;
	protected List<BaseDrillDisplay> outputs = new();
	private static bool portrait = false;

	private void OnEnable()
	{
		appController = FindObjectOfType<ApplicationController>();
		ToggleSprite();
	}

	public void ShowSessionData(Date Date, string Summary, string Age, int Time)
	{
		date.text = Date.LongDate();
		summary.text = Summary;
		ageText.text = Age;
		time.text = Time + "min";
	}

	public void ToggleLayouts()
	{
		portrait = !portrait;

		foreach (BaseDrillDisplay item in outputs)
		{
			item.ToggleLayoutType();
		}

		ToggleSprite();
	}

	private void ToggleSprite()
	{
		Vector3 angles = layoutToggle.transform.localEulerAngles;
		angles.z = portrait ? 90 : 0;
		layoutToggle.transform.localEulerAngles = angles;
	}

	protected void SetAllLayouts()
	{
		foreach (BaseDrillDisplay item in outputs)
		{
			if (item.Portrait != portrait)
			{
				item.ToggleLayoutType();
			}
		}
	}
}