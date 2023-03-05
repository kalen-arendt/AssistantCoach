using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionLabel : MonoBehaviour
{
	[SerializeField] SpriteRenderer fill = null;
	[SerializeField] Text label = null;

	public void SetFill (Color color)
	{
		fill.color = color;
	}

	public void SetLabel (string str)
	{
		label.text = str;
	}
}