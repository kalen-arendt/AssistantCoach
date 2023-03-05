using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
[SelectionBase]
public class TBoardPlayer : MonoBehaviour {


	[SerializeField] Team team = Team.Attacking;
	[SerializeField] Material lineMaterial = null;
	[SerializeField] Button button = null;


	private PlayerPathing pathing;
	private SpriteRenderer sprite;

	private static float size = 5;

	private static TBoardPlayer selected = null;

	private readonly Color attackingColor = Color.red;
	private readonly Color defendingColor = Color.cyan;

	private const string MARKUP_LAYER = "Tactics Markings";
	private const float LINE_WIDTH = 0.5F;
	private const float LINE_OPACITY = 0.6F;

	private const float DISTANCE_THRESHOLD = 1;



	public enum Team {
		Attacking,
		Defending
	}


	void OnEnable () {
		TacticsBoardManager.OnPlay += OnPlay;
		TacticsBoardManager.OnPause += OnPause;
		TacticsBoardManager.OnStop += OnReset;

	}

	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponentInChildren<SpriteRenderer> ();
		if (sprite != null) {
			sprite.color = teamColor;
			sprite.transform.localScale = size* Vector3.one;
		}
		pathing = GetComponentInChildren<PlayerPathing> ();
		if (pathing != null)
			pathing.SetPlayer(this);

		button = GetComponentInChildren<Button> ();
		if (button != null)
			button.onClick.AddListener(delegate() {
				OnSelect();
			});

	}

	void OnSelect () {
		TacticsBoardManager.SetSelectedPlayer(this);
	}

	void OnPlay () {
		TacticsBoardManager.OnUpdate += Step;
	}

	void OnPause () {
		TacticsBoardManager.OnUpdate -= Step;
	}

	void OnReset () {
		pathing.Reset();
		Step ();
	}

	void Step () {
		sprite.transform.position = pathing.nextPosition();
	}


	void SetupLineRenderer () {
		if (pathing == null)
			return;

		LineRenderer line = GetComponent<LineRenderer> ();
		int count = pathing.transform.childCount;
		line.positionCount = count;
		Vector3[] positions = new Vector3[count];

		for (int i = 0; i < count; i++) {
			positions[i] = pathing.transform.GetChild(i).position;
			//line.SetPosition(i, pathing.GetChild(i).position);
		}

		line.SetPositions(positions);

		Color lineCol = new Color (teamColor.r, teamColor.g, teamColor.b, LINE_OPACITY);

		line.material = lineMaterial;
		line.startColor = teamColor;
		line.endColor = teamColor;
		line.startWidth = LINE_WIDTH;
		line.endWidth = LINE_WIDTH;
		line.useWorldSpace = true;
		line.sortingLayerName = MARKUP_LAYER;
	}


	public Color teamColor {
		get {
			if (team == Team.Attacking)
				return attackingColor;
			else
				return defendingColor;
		}
	}

}
