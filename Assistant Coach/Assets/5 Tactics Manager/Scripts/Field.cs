using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Field : MonoBehaviour {

	[SerializeField] Sprite grassTile = null;
	[SerializeField] Color grassColorOverlay = Color.white;
	[SerializeField] Material lineMaterial = null;



	const int WIDTH = 70, LENGTH = 110;
	const int GOAL_WIDTH = 8;
	const int PENALTY_AREA = 18;
	const int GOAL_AREA = 6;

	const float LINE_WIDTH = 0.6f;
	const float GOAL_HEIGHT = 8f/3;

	const float SCALE = 10;

	const int 	GRASS_LAYER = -2, LINES_LAYER = -1;
	const string FIELD_LAYER = "Field";



	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(SCALE, SCALE, 0);
		SpriteRenderer sp = GetComponent<SpriteRenderer> ();

		sp.sprite = grassTile;
		sp.drawMode = SpriteDrawMode.Tiled;
		sp.tileMode = SpriteTileMode.Adaptive;
		sp.sortingLayerName = FIELD_LAYER;
		sp.sortingOrder = GRASS_LAYER;
		sp.size = new Vector2((float)WIDTH/SCALE, (float)LENGTH/SCALE);


		SetupFieldMarkings();
	}


	void SetupFieldMarkings () {
		DrawOutline();
		DrawGoalAreas();
		DrawPenaltyAreas();
	}


	void DrawOutline () {

		// OUTLINE ----------------------------------------------------
		LineRenderer outline = new GameObject("Outline").AddComponent<LineRenderer> ();
		StandardizeLine(outline);

		Vector3[] positions;

		positions = new Vector3[] {
			new Vector2(-WIDTH/2, +LENGTH/2),
			new Vector2(+WIDTH/2, +LENGTH/2),
			new Vector2(+WIDTH/2, -LENGTH/2),
			new Vector2(-WIDTH/2, -LENGTH/2)
		};

		outline.positionCount = positions.Length;
		outline.SetPositions(positions);
		outline.loop = true;


		// HALFWAY LINE -----------------------------------------------------

		LineRenderer halfwayLine = new GameObject("HalfwayLine").AddComponent<LineRenderer> ();
		StandardizeLine(halfwayLine);

		halfwayLine.SetPositions(
			new Vector3[] {
				new Vector2(-WIDTH/2, 0),
				new Vector2(+WIDTH/2, 0),
			}
		);
	}

	void DrawGoalAreas () {

		CreateBox(new GameObject("Attacking Goal Area").AddComponent<LineRenderer>(), GOAL_AREA, +1);
		CreateBox(new GameObject("Defending Goal Area").AddComponent<LineRenderer>(), GOAL_AREA, -1);
	}

	void DrawPenaltyAreas () {
		CreateBox(new GameObject("Attacking Penalty Box").AddComponent<LineRenderer>(), PENALTY_AREA, +1);
		CreateBox(new GameObject("Defending Penalty Box").AddComponent<LineRenderer>(), PENALTY_AREA, -1);
	}

	void CreateBox (LineRenderer line, int distance, int direction) {
		float dir = Mathf.Sign(direction);
		float right = GOAL_WIDTH/2 + distance;
		float top = LENGTH/2*dir;


		Vector3[] positions = new Vector3[] {
			new Vector2 (right, top),
			new Vector2 (right, top - distance*dir),
			new Vector2 (-right, top - distance*dir),
			new Vector2 (-right, top)
		};

		line.positionCount = positions.Length;
		StandardizeLine(line);
		line.SetPositions(positions);
	}

	void StandardizeLine (LineRenderer line) {
		line.material = lineMaterial;
		line.startColor = Color.white;
		line.endColor = Color.white;
		line.startWidth = LINE_WIDTH;
		line.endWidth = LINE_WIDTH;
		line.useWorldSpace = true;
		line.sortingOrder = LINES_LAYER;
		line.transform.SetParent(transform);
	}
}
