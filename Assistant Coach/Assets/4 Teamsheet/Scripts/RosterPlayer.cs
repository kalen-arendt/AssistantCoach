using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RosterPlayer : MonoBehaviour
{
	[SerializeField] InputField inputField = null;

	Roster roster;
	Player player = null;

	public Roster Roster { get { return roster; } set { roster = value; }}

	void Start ()
	{
		inputField.onEndEdit.AddListener(delegate {
			SetPlayerName ();
			PlayerPrefsManager.TeamSheet.SavePlayer(player);
		});
	}

	void SetPlayerName ()
	{
		player.Name = inputField.text;
	}

	void DisplayName ()
	{
		inputField.text = player.Name;
	}

	public Player Player {
		get {
			return player;
		}
		set {
			player = value;
			DisplayName();
		}
	}
}

