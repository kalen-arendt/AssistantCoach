using System.Collections.Generic;

using UnityEngine;

public class SavedSessionPreviewManager : MonoBehaviour
{
	[SerializeField] private Transform parent = null;
	[SerializeField] private GameObject Template = null;

	[SerializeField] private GameObject confirmationWindow = null;
	[SerializeField] private Message messageWindow = null;
	private List<SavedSessionPreview> SavedSessionArray { get; } = new();
	private const string MSG_CONFIRM_CAN_DELTE = "Permanetly Delete All Sessions?";
	private const string MSG_DELETION_COMPLETED = "Sessions have been deleted";

	private void OnEnable()
	{
		UpdateSavedSessions();
	}

	public void UpdateSavedSessions()
	{
		SessionData[] dataArray = PlayerPrefsManager.SavedSessions.GetSessionDescriptions ();

		while (dataArray.Length < SavedSessionArray.Count)
		{
			SavedSessionPreview v = SavedSessionArray [^1];
			SavedSessionArray.RemoveAt(SavedSessionArray.Count - 1);
			Destroy(v.gameObject);
		}

		for (var i = 0; i < dataArray.Length; i++)
		{
			if (i >= SavedSessionArray.Count)
			{
				SavedSessionArray.Add(Instantiate(Template, parent).GetComponent<SavedSessionPreview>());
			}

			SavedSessionPreview previewObj = SavedSessionArray [i];
			previewObj.SessionData = dataArray[i];
			previewObj.UpdateObject();
		}
	}

	public void DeleteButton()
	{
		UserConfirmation msg_window = Instantiate (confirmationWindow).GetComponent<UserConfirmation>();
		msg_window.SetButtons(DeleteAllSessions, MSG_CONFIRM_CAN_DELTE);
	}

	private void DeleteAllSessions()
	{
		foreach (SessionData session in PlayerPrefsManager.SavedSessions.GetSessionDescriptions())
		{
			PlayerPrefsManager.SavedSessions.RemoveSession(session.index);
		}

		UpdateSavedSessions();

		Instantiate(messageWindow).ShowMessage(MSG_DELETION_COMPLETED);
	}
}