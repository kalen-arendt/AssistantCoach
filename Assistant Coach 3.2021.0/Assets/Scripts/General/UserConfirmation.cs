using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(GraphicRaycaster))]
public class UserConfirmation : MonoBehaviour
{
	[SerializeField] private Text Summary = null;
	[SerializeField] private Button confirm = null;
	[SerializeField] private Button reject = null;

	private void OnEnable()
	{
		confirm.onClick.AddListener(() => Destroy(gameObject));
		reject.onClick.AddListener(() => Destroy(gameObject));
	}

	public void SetButtons(UnityAction onConfirmationFunction, string summary)
	{
		confirm.onClick.AddListener(onConfirmationFunction);
		Summary.text = summary;
	}
}