using UnityEngine;

namespace AssistantCoach.UI
{
	public class TimelineVisualizer : MonoBehaviour, ITimelineVisualizer
	{
		public RectTransform FillRect { get; private set; }


		void Awake ()
		{
			FillRect = GetComponent<RectTransform>();
		}
	}
}