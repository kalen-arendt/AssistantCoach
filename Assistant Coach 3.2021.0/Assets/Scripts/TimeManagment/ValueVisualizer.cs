using UnityEngine;
using UnityEngine.UI;

namespace AssistantCoach.UI
{
	[RequireComponent(typeof(IUpdatableValue))]
	public class ValueVisualizer : MonoBehaviour
	{
		[SerializeField] private Text text = null;
		private IUpdatableValue updatableValue;

		private void Awake()
		{
			updatableValue = GetComponent<IUpdatableValue>();
		}

		private void OnEnable()
		{
			updatableValue.OnValueChanged += UpdatableValue_OnValueChanged;
		}

		private void OnDisable()
		{
			updatableValue.OnValueChanged -= UpdatableValue_OnValueChanged;
		}

		private void UpdatableValue_OnValueChanged(int value)
		{
			text.text = value.ToString();
		}
	}
}