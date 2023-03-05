using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TacticsBoardManager : MonoBehaviour {

	[SerializeField] Button selectionMask = null;
	[SerializeField] Button playButton = null;
	[SerializeField] Button pauseButton = null;


	Camera mainCamera = null;

	public delegate void PlayModeEvent ();
	public static event PlayModeEvent OnPlay;
	public static event PlayModeEvent OnPause;
	public static event PlayModeEvent OnStop;
	public static event PlayModeEvent OnUpdate;


	private static ExecutionState executionState = ExecutionState.Pause;
	private static Tool selectedTool = Tool.none;

	public enum Tool {
		none,
		moveSelected,
		pathing,
		arrows,
		zones
	}


	void Start () {
		mainCamera = Camera.main;

		selectionMask = Instantiate(selectionMask);
		selectionMask.onClick.AddListener(MouseClickedOnField);

		playButton.onClick.AddListener(Play);
		pauseButton.onClick.AddListener(Pause);
	}

	void Update () {
		// calls a subscription update
		if (OnUpdate!= null)
			OnUpdate();


		//DrawMouseRay();
	}


	private void MouseClickedOnField () {
		Vector3 mousePos = MouseWorldPosition();



		print("MousePos: " + mousePos);
	}

	void DrawMouseRay () {
		Vector3 mPos = MouseWorldPosition();
		Vector3 dir = mainCamera.transform.forward;

		var pos = Vector3.ProjectOnPlane(mPos, Vector3.back);


		Debug.DrawRay(mPos, dir, Color.black, 3);

		Debug.DrawRay(mPos + new Vector3 (0,0, 5), Vector3.right, Color.black);
	}

	private Vector3 MouseWorldPosition () {
		var pos = mainCamera.ScreenToWorldPoint(new Vector3(
			Input.mousePosition.x,
			Input.mousePosition.y,
			-mainCamera.transform.position.z
		));


		//pos = Vector3.o

		return pos;
	}



	public void SetTool (Tool tool) {
		selectedTool = tool;
	}



	#region Static Methods to call Execution State Events

	public enum ExecutionState {
		Play,
		Pause
	}


	public static void Play () {
		if (executionState != ExecutionState.Play) {
			executionState = ExecutionState.Play;
			if (OnPlay != null)
				OnPlay ();
		}	
	}

	public static void Pause () {
		if (executionState != ExecutionState.Pause) {
			executionState = ExecutionState.Pause;
			if (OnPause != null)
				OnPause ();
		}
	}

	public static void Reset () {
		if (OnStop!= null)
			OnStop ();

		if (executionState == ExecutionState.Play)
			Pause();		
	}

	#endregion

	private static TBoardPlayer selected = null;

	public static void SetSelectedPlayer (TBoardPlayer selectedPlayer) {

		if (selectedTool == Tool.none)
		{
			if (selected != selectedPlayer)
				selected = selectedPlayer;
			else
				selected = null;
		}


	}

	public static TBoardPlayer GetSelectedPlayer () {
		return selected;
	}



}
