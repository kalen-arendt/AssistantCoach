using System.Collections.Generic;
using System.Linq;

using AssistantCoach;
using AssistantCoach.UI;

using UnityEngine;

namespace Assets.Scripts.TimeManagment
{
	public class SessionTimelineManager : MonoBehaviour
	{
		[SerializeField] List<IncrementalTimeSelector> timeSelectors = new List<IncrementalTimeSelector>();

		Page page = null;


		private List<int> Times {
			get => timeSelectors.Select(t => t.Value).ToList();
			set => timeSelectors.ForEach((item, index) => item.SetValue(value[index]));
		}


		void Awake()
		{
			page = GetComponentInParent<Page>();
			Debug.Log(page.gameObject.name);
		}

		void OnEnable()
		{
			page.OnPageOpened += Page_OnPageOpened;
			page.OnSubmit += Page_OnSubmit;
		}

		void OnDisable ()
		{
			page.OnPageOpened -= Page_OnPageOpened;
			page.OnSubmit -= Page_OnSubmit;
		}


		private void Page_OnPageOpened (Page obj)
		{
			Times = PlayerPrefsManager.Settings.Timeline;
		}

		private void Page_OnSubmit (Page obj)
		{
			Debug.Log("Page_OnSubmit!");
			PlayerPrefsManager.Settings.Timeline = Times;
		}
	}
}