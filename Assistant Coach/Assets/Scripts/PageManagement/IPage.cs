using System;

namespace AssistantCoach
{
	public interface IPage
	{
		event Action<Page> OnSubmit;
		event Action<Page> OnPageInitialized;
		event Action<Page> OnPageClosed;
		event Action<Page> OnPageOpened;

		void Initialize ();
		void Close ();
		//void CloseSilently ();
		void Open ();
		//void OpenSilently ();
	}
}