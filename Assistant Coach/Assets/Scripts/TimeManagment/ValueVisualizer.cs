using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace AssistantCoach.UI
{
	[RequireComponent(typeof(IUpdatableValue))]
	public class ValueVisualizer : MonoBehaviour
	{
		[SerializeField] Text text = null;


		IUpdatableValue updatableValue;

		void Awake()
		{
			updatableValue = GetComponent<IUpdatableValue>();
		}

		void OnEnable()
		{
			updatableValue.OnValueChanged += UpdatableValue_OnValueChanged;
		}

		void OnDisable()
		{
			updatableValue.OnValueChanged -= UpdatableValue_OnValueChanged;
		}

		private void UpdatableValue_OnValueChanged (int value)
		{
			text.text = value.ToString ();
		}
	}
}