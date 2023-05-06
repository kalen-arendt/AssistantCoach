using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
	[SerializeField] Text text = null;
	public void ShowMessage (string str) { text.text = str; }

	void OnEnable ()
	{
		var canvas = GetComponent<Canvas> ();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.sortingOrder = 1000;
	}
}
