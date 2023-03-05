using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [ExecuteInEditMode]
public class TeamSheetManager : MonoBehaviour
{
	[Header ("Formation")]
	[SerializeField] Formation formation = null;

	[Header ("Setup")]
	[SerializeField] Transform keeperRoot = null;
	[SerializeField] Transform defendersRoot = null;
	[SerializeField] Transform midfieldersRoot = null;
	[SerializeField] Transform forwardsRoot = null;

	[Header ("Prefabs")]
	[SerializeField] PositionLabel positionMarker = null;


	List<PlayerDisplay> playerDisplays = new List<PlayerDisplay> ();
	Roster roster;

	void Awake ()
	{
		Clear ();
	}

	void Start ()
	{
		SetUpTeamSheet();
	}

	void Update () {
		Clear ();
		SetUpTeamSheet();
	}

	void Clear () {
		ClearPreviousMarkers(keeperRoot);
		ClearPreviousMarkers(defendersRoot);
		ClearPreviousMarkers(midfieldersRoot);
		ClearPreviousMarkers(forwardsRoot);
	}

	void SetUpTeamSheet ()
	{
		//Keeper
		SetLine(formation.Keeper, keeperRoot, 0);

		//Defenders
		SetLine(formation.Defenders, defendersRoot, formation.DefenceSpacing);

		//Midfielders
		SetLine (formation.Midfielders, midfieldersRoot, formation.MidfieldSpacing);
		
		//Forwards
		SetLine(formation.Forwards, forwardsRoot, formation.ForwardSpacing);

		SetPlayers();
	}

	void SetLine (List<Formation.Position> line, Transform root, float spacing)
	{
		int quantity = line.Count;
		int spaces = quantity - 1;
		float offset = spaces * -0.5f * spacing;


		for (int i = 0; i < quantity; i++) {
			var positionObj = Instantiate (positionMarker, root) as PositionLabel;
			var player = positionObj.GetComponent<PlayerDisplay> ();
			var position = line [i];
			positionObj.SetLabel (position.Role.ToString ());
			positionObj.SetFill (position.Color);
			positionObj.transform.localPosition = new Vector2 (offset, 0) + position.Offset;
			offset += spacing;

			playerDisplays.Add(player);
		}
	}

	void SetPlayers ()
	{
		roster = FindObjectOfType<Roster> ();

		var players = roster.Players;

		for (int i = 0; i < players.Count && i < playerDisplays.Count; i++) {
			Debug.Log(players[i].Name); 
			playerDisplays[i].Player = players[i];
		}
	}

	void ClearPreviousMarkers (Transform rootTransform)
	{
		if (rootTransform == null) return;

		for (int i = rootTransform.childCount - 1; i >= 0; i--) {
			DestroyImmediate(rootTransform.GetChild(i).gameObject);
		}
	}
}



public class Player
{
	public readonly string id;
	string name;
	List<Formation.position> positions;	

	public Player ()
	{
		string random_id = "";
		for (int i = 0; i < 6; i++) { random_id += Random.Range(0,10).ToString(); }

		this.id = random_id;
		this.name = "";
		this.positions = new List<Formation.position> ();
	}

	public Player (string id, string name)
	{
		this.id = id;
		this.name = name;
		this.positions = new List<Formation.position> ();
	}

	public Player (string id, string name, List<Formation.position> positions)
	{
		this.id = id;
		this.name = name;
		this.positions = positions;
	}


	public string Name {
		get { return name; }
		set { name = value;}
	}

	public List<Formation.position> Positions {
		get { return positions; }
		set { positions = value;}
	}



}
