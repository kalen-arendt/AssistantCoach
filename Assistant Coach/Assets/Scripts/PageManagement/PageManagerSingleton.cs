using UnityEngine;

namespace AssistantCoach
{
	public class PageManagerSingleton : MonoBehaviour
	{
		public static ILocalPageManager CurrentPageManager { get; set; }
		

		public void SetPage (int index)
		{
			CurrentPageManager?.SwitchToPage(index);
		}

		public void PageNext ()
		{
			CurrentPageManager?.PageNext();
		}

		public void PagePrevious ()
		{
			CurrentPageManager?.PagePrevious();
		}
	}
}