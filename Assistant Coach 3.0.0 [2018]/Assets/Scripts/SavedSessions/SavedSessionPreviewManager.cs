using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavedSessionPreviewManager : MonoBehaviour
{
	[SerializeField] Transform parent = null;
	[SerializeField] GameObject Template = null;

	[SerializeField] GameObject confirmationWindow = null;
	[SerializeField] Message messageWindow = null;


	List<SavedSessionPreview> savedSessionArray = new List<SavedSessionPreview> ();
	const string MSG_CONFIRM_CAN_DELTE = "Permanetly Delete All Sessions?";
	const string MSG_DELETION_COMPLETED = "Sessions have been deleted";


	void OnEnable ()
	{
		UpdateSavedSessions ();
	}

	public void UpdateSavedSessions ()
	{
		SessionData[] dataArray = PlayerPrefsManager.SavedSessions.GetSessionDescriptions ();

		while (dataArray.Length < savedSessionArray.Count)
		{
			var v = savedSessionArray [savedSessionArray.Count - 1];
			savedSessionArray.RemoveAt (savedSessionArray.Count - 1);
			Destroy (v.gameObject);
		}

		for (int i = 0;i < dataArray.Length; i++)
		{
			if (i >= savedSessionArray.Count)
			{
				savedSessionArray.Add(Instantiate (Template, parent).GetComponent<SavedSessionPreview> ());
			}

			var previewObj = savedSessionArray [i];
			previewObj.SessionData = dataArray [i];
			previewObj.UpdateObject ();
		}
	}

	public void DeleteButton ()
	{
		var msg_window = (Instantiate (confirmationWindow)).GetComponent<UserConfirmation>();
		msg_window.SetButtons (DeleteAllSessions, MSG_CONFIRM_CAN_DELTE);
	}

	void DeleteAllSessions ()
	{
		foreach (SessionData session in PlayerPrefsManager.SavedSessions.GetSessionDescriptions ()) {
			PlayerPrefsManager.SavedSessions.RemoveSession (session.index);
		}

		UpdateSavedSessions ();

		Instantiate (messageWindow).ShowMessage (MSG_DELETION_COMPLETED);
	}
}