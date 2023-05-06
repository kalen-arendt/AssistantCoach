using UnityEngine;

namespace AssistantCoach.UI
{
	public class IncrementalTimeSelector : IncrementalSelector
	{
		[SerializeField] int minValue = 10;
		[SerializeField] int initialValue = 20;
		[SerializeField] int maxValue = 30;
		[SerializeField] int increment = 5;

		protected override int MinValue => minValue;
		protected override int InitialValue => initialValue;
		protected override int MaxValue => maxValue;
		protected override int Increment => increment;
	}
}