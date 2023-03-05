
namespace Tactics {


	
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.Events;
	using UnityEngine.EventSystems;

	[RequireComponent(typeof(Slider))]
	public class TacticsTimeSlider : MonoBehaviour, IInitializePotentialDragHandler, IEndDragHandler, IDeselectHandler {


		[SerializeField] Text
			minValueText = null,
			currentValueText = null,
			maxValueText = null;

		Slider slider = null;

		private float 
			minTime = 0,
			maxTime = 4;

		private const float
			TIME_SPAN_RANGE = 4;

		public static TacticsTimeSlider
			instance { get; set; }

		private bool
			minTextUpdated = false,
			maxTextUpdated = false;

		/*void OnValidate () {  // this sort of auto sets stuff in the editor
			slider = GetComponent<Slider> ();
			minTextUpdated = false;
			maxTextUpdated = false;
			if (slider) {
				if (!minTextUpdated && minValueText) {
					minValueText.text = "" + 0f.ToString("F1");
					minTextUpdated = true;
				}

				if (!maxTextUpdated && maxValueText) {
					maxValueText.text = "" + (0f + TIME_SPAN_RANGE).ToString("F1");
					maxTextUpdated = true;
				}
			}
		}*/

//		public TacticsTimeSlider (Text min, Text current, Text max) {
//			minValueText = min;
//			currentValueText = current;
//			maxValueText = max;
//		}


		protected void Awake () {
			if (instance == null)
				instance = this;
			else
				DestroyImmediate(this);
		}


		protected void OnEnable () {
			Debug.Log("slider: " + slider, this);
			if (!slider)
				slider = GetComponent<Slider>();

			slider.onValueChanged.AddListener(UpdateCurrentTimeText);
		}

		protected void OnDisable () {
			slider.onValueChanged.RemoveListener(UpdateCurrentTimeText);
		}


		void UpdateCurrentTimeText (float time) {
//			var time = slider.value;
			TacticsManager.time = time;
			currentValueText.text = time.ToString("F1");
		}

		public void OnDeselect (BaseEventData eventData)
		{
			print("Deselected");
			UpdateSliderValues ();
		}

		public void OnDrag (PointerEventData eventData)
		{
			print("Being dragged around");
		}
			

		public void OnInitializePotentialDrag (PointerEventData eventData) {
			Debug.Log("OnInitializePotentialDrag", this);
			UpdateSliderValues ();
		}




		public void OnEndDrag (PointerEventData data) {
			Debug.Log("OnEndDrag", this);
			UpdateSliderValues ();
		}


		public void UpdateSliderValues () {
			float min = Mathf.Clamp(slider.value - TIME_SPAN_RANGE/2, 0, Mathf.Infinity);
			float max = min + TIME_SPAN_RANGE;
			slider.minValue = min;
			slider.maxValue = max;

			minValueText.text = min.ToString("F1");
			maxValueText.text = max.ToString("F1");
		}
	}
}

//	public class TimelineSlider : MonoBehaviour {
//		[SerializeField] Text minValueText = null;
//		[SerializeField] Text currentValueText = null;
//		[SerializeField] Text maxValueText = null;
//
//		void Start () {
//			TacticsTimeSlider timeSlider = gameObject.AddComponent<TacticsTimeSlider> ();
//			timeSlider = new TacticsTimeSlider(minValueText, currentValueText, maxValueText);
//		}
//	}
