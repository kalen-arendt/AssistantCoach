using System;

using UnityEngine;
using UnityEngine.Events;

namespace AssistantCoach.UI
{
	[Serializable]
	public abstract class IncrementalSelector : MonoBehaviour, IIncrementalSelector
	{
		public event UnityAction<int> OnValueChanged;

		protected abstract int MinValue { get; }
		protected abstract int InitialValue { get; }
		protected abstract int MaxValue { get; }
		protected abstract int Increment { get; }



		public int Value { get; private set; }

		private void Start()
		{
			SetValue(InitialValue);
		}

		public void SetValue(int value)
		{
			value = Mathf.Clamp(value, MinValue, MaxValue);

			if (Value != value)
			{
				Value = value;
				OnValueChanged?.Invoke(Value);
			}
		}

		public void IncrementValue()
		{
			SetValue(Value + Increment);
		}

		public void DecrementValue()
		{
			SetValue(Value - Increment);
		}
	}
}