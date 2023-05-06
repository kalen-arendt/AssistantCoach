namespace AssistantCoach
{
	public interface ILocalPageManager
	{
		void PageNext ();
		void PagePrevious ();
		void SwitchToPage (int index);
	}
}