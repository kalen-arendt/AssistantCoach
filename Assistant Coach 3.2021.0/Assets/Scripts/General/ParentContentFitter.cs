using System.Collections.Generic;
using System.Linq;

using My.Unity.Extensions;

using UnityEngine;
using UnityEngine.UI;

public class ParentContentFitter : MonoBehaviour
{
	private ChildContentFitter childContentFitter;
	private RectTransform rectTransform;

	private int framesSinceRecalculation = 0;
	private const int RECALCULATE_AFTER_FRAMES = 30;


	private void OnEnable()
	{
		childContentFitter = GetComponentInChildren<ChildContentFitter>();
		childContentFitter.OnDimentionsChangedEvent += ChangeDimentions;

		rectTransform = (RectTransform)GetComponent<Transform>();
	}


	private void LateUpdate()
	{
		framesSinceRecalculation++;
		if (framesSinceRecalculation >= RECALCULATE_AFTER_FRAMES)
		{
			GetComponentsInParent<LayoutGroup>().Reverse().ToList().ForEach(layout =>
			{
				layout.CalculateLayoutInputHorizontal();
				layout.SetLayoutVertical();
			});
		}
	}
	

	public void ChangeDimentions(Vector2 SizeDelta)
	{
		rectTransform.Coalesce(transform);

		if (rectTransform.pivot.y != 1)
		{
			rectTransform.pivot = new Vector2(0.5f, 1);
		}

		if (SizeDelta.y == rectTransform.rect.y)
		{
			return;
		}

		SizeDelta.x = rectTransform.sizeDelta.x;
		rectTransform.sizeDelta = SizeDelta;
	}
}