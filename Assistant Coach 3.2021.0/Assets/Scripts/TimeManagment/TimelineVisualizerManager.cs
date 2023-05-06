using System.Linq;

using UnityEngine;

namespace AssistantCoach.UI
{
	public class TimelineVisualizerManager : MonoBehaviour
	{
		[SerializeField] private Transform timelineParent = null;
		private IIncrementalSelector[] timelineSelectors;
		private ITimelineVisualizer[] timelineVisualizers;

		private void Awake()
		{
			timelineSelectors = timelineParent.GetComponentsInChildren<IIncrementalSelector>();
			timelineVisualizers = timelineParent.GetComponentsInChildren<ITimelineVisualizer>();

			if (timelineSelectors.Length != timelineVisualizers.Length)
			{
				Debug.LogError(
					$"There are {timelineSelectors.Length} timeline selectors" +
					$"and {timelineVisualizers.Length} timeline visualizers." +
					$"These values must match!"
				);
				Destroy(this);
				return;
			}
		}

		private void OnEnable()
		{
			foreach (IIncrementalSelector selector in timelineSelectors)
			{
				selector.OnValueChanged += TimeSelector_OnValueChanged;
			}
		}

		private void TimeSelector_OnValueChanged(int time)
		{
			var totalTime = timelineSelectors
									.Select((item, index) => item.Value)
									.Aggregate((total, next) => total += next);

			var sumValue = 0;
			float sumFraction = 0;

			for (var i = 0; i < timelineSelectors.Length; i++)
			{
				var start = sumFraction;
				sumValue += timelineSelectors[i].Value;
				sumFraction = sumValue / (float)totalTime;

				RectTransform rect = timelineVisualizers[i].FillRect;
				rect.anchorMin = new Vector2(start, 0);
				rect.anchorMax = new Vector2(sumFraction, 1);
			}
		}
	}
}