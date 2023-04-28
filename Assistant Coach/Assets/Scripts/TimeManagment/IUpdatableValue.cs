using UnityEngine.Events;

namespace AssistantCoach.UI
{
	public interface IUpdatableValue
	{
		event UnityAction<int> OnValueChanged;
	}
}