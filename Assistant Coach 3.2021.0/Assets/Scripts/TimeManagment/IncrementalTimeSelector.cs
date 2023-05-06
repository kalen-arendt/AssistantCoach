using UnityEngine;

namespace AssistantCoach.UI
{
	public class IncrementalTimeSelector : IncrementalSelector
	{
		[SerializeField] private int minValue = 10;
		[SerializeField] private int initialValue = 20;
		[SerializeField] private int maxValue = 30;
		[SerializeField] private int increment = 5;

		protected override int MinValue => minValue;
		protected override int InitialValue => initialValue;
		protected override int MaxValue => maxValue;
		protected override int Increment => increment;
	}
}