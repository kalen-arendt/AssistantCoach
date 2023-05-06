namespace AssistantCoach.UI
{
	public interface IIncrementalSelector : IUpdatableValue
	{
		int Value { get; }
		void DecrementValue();
		void IncrementValue();
		void SetValue(int value);
	}
}