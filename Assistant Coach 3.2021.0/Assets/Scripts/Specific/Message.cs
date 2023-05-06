using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
	[SerializeField] private Text text = null;
	public void ShowMessage(string str) { text.text = str; }

	private void OnEnable()
	{
		Canvas canvas = GetComponent<Canvas> ();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.sortingOrder = 1000;
	}
}
