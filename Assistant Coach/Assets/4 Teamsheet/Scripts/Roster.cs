using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roster : MonoBehaviour
{
	[SerializeField] Transform parent = null;
	[SerializeField] GameObject rosterPlayerPrefab = null;
	[SerializeField] GameObject addPlayerButton = null;
	[SerializeField] GameObject saveRosterButton = null;

	List<Player> players = new List<Player> ();

	public List<Player> Players { get { return players; }}


	void Awake ()
	{
		players = PlayerPrefsManager.TeamSheet.GetRosterPlayers();

		foreach (var player in players)
		{
			var rosterPlayer = CreateRosterPlayer (); 
			rosterPlayer.Player = player;
			rosterPlayer.Roster = this;
		}

		SetButtonsBottom ();

		addPlayerButton.GetComponent<Button>().onClick.AddListener(
			delegate { AddNewPlayerToRoster(); }
		);

		saveRosterButton.GetComponent<Button>().onClick.AddListener(
			delegate { SaveRoster(); }
		);
	}

	RosterPlayer CreateRosterPlayer ()
	{
		var obj = Instantiate (rosterPlayerPrefab, parent);
		return obj.GetComponent<RosterPlayer>();
	}

	public void AddNewPlayerToRoster ()
	{
		var rosterPlayer = CreateRosterPlayer ();
		var player = new Player();
		players.Add(player);
		rosterPlayer.Player = player;
		rosterPlayer.Roster = this;

		SetButtonsBottom ();
		SaveRoster();
	}

	void SetButtonsBottom ()
	{
		addPlayerButton.transform.SetAsLastSibling();
		saveRosterButton.transform.SetAsLastSibling();
	}

	void SaveRoster ()
	{
		PlayerPrefsManager.TeamSheet.SetRosterPlayers(players);
	}
}
