

namespace Tactics {

	using System.Collections;
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class TacticsManager : MonoBehaviour {

		[SerializeField] Button playButton = null;
		[SerializeField] Button addKeyMomentButton = null;

		//private static TacticsTimeSlider timeSlider = null;
		private static TacticsManager instance = null;

//		public delegate void TacticsKeyMoment(float time);
		public delegate void TimeUpdate (float time);
		public static event TimeUpdate newKeyMoment;
		public static event TimeUpdate updatePositions;

		private static State currentState = State.TacticsEditor;
		public static float time {get; set;}

		enum State {
			PlayMode,
			Puased,
			TacticsEditor
		}


		// Use this for initialization
		void Awake () {
			if (instance != null)
				DestroyImmediate(this);
			else
				instance = this;
		}
		// subsribe to all necesary listeners
		void OnEnable () {
//			timeSlider = TacticsTimeSlider.instance;

			try {
				addKeyMomentButton.onClick.AddListener(delegate {
						newKeyMoment(time);
				});
			}  catch (NullReferenceException nfe) {
				Debug.LogWarning("Play button is null" + nfe.StackTrace, this);
			}

			try {
				playButton.onClick.AddListener(
					delegate { toggleState();}
				);
			} catch (NullReferenceException nfe) {
				Debug.LogWarning("Play button is null" + nfe.StackTrace, this);
			}

			try {
//				TacticsTimeSlider.instance.onValueChanged.AddListener(SetTimeAndUpdate);
			} catch (Exception e) {
				Debug.LogWarning("No TimeSlider attached to TacticsManager\n" + e.Message, this);
			}
		}

		static void toggleState () {
			if (currentState == State.TacticsEditor || currentState == State.Puased)
				currentState = State.PlayMode;
			else
				currentState = State.TacticsEditor;
		}

		void Update () {
			if (currentState == State.PlayMode) {
				time += Time.deltaTime;
				CallPositionUpdate ();
			}
		}

//		public void SetTimeAndUpdate (float time) {
//			TacticsManager.time = time;
//			var min = Mathf.Clamp(time-2, 0, time-2);
//			timeSlider.minValue = min;
//			timeSlider.maxValue = min+4;
//			timeSlider.value = time;
//
//			CallPositionUpdate ();
//		}

		private static void CallPositionUpdate () {
			if (updatePositions != null)
				updatePositions(time);
		}





	}

}
