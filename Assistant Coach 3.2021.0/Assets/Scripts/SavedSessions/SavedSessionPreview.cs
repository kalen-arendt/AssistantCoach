﻿using UnityEngine;
using UnityEngine.UI;

public class SavedSessionPreview : MonoBehaviour
{
	[SerializeField] private Text summary = null;
	[SerializeField] private Text date = null;


	public SessionData SessionData { get; set; }


	public void UpdateObject()
	{
		summary.text = SessionData.summary;
		date.text = SessionData.date.ShortDate();
	}

	public void ShowSession()
	{
		FindObjectOfType<SavedSessionOutputController>().ShowSavedSession(SessionData.index);
	}

	public void DeleteSession()
	{
		PlayerPrefsManager.SavedSessions.RemoveSession(SessionData.index);
		FindObjectOfType<SavedSessionPreviewManager>().UpdateSavedSessions();
	}
}