using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationController : MonoBehaviour
{
	Canvas currentCanvas, nextCanvas;
	RectTransform tranRect, currentRect, nextRect;

	Vector2 offsetMax, offsetMin;

	float progress, distance, x_offset;

	const float transitionDuration = 0.5f;
	const int BOARDER = 0;

	bool transitioning = false;

	readonly Vector2 NORMAL_MIN_ANCHOR = new Vector2 (0, 0);
	readonly Vector2 NORMAL_MAX_ANCHOR = new Vector2 (1, 1);


	enum Transition {next, prev}


	void Start ()
	{
		currentCanvas = GetComponentInChildren<LevelManager> ().GetComponent<Canvas> ();
		currentRect = currentCanvas.GetComponent<RectTransform>();
	}

	public void CallNext (Canvas canvas) {
		CallNextA(canvas);
	}

	public void CallPrevious (Canvas canvas) {
		CallPreviousA(canvas);
	}


	public void CallNextA (Canvas canvas)
	{
		InitializeTransitionA(Transition.next, canvas);
	}

	public void CallPreviousA (Canvas canvas)
	{
		InitializeTransitionA(Transition.prev, canvas);
	}

	public void CallNextB (Canvas canvas)
	{
		InitializeTransitionB(Transition.next, canvas);
	}

	public void CallPreviousB (Canvas canvas)
	{
		InitializeTransitionB(Transition.prev, canvas);
	}

	// Transitions as if adding/removing cards on top
	void InitializeTransitionA (Transition transitionType, Canvas canvas)
	{
		if (transitioning) { return; }

		nextCanvas = canvas;
		nextRect = nextCanvas.GetComponent<RectTransform>();

		progress = 0;

		var width = currentRect.rect.width;
		bool next = transitionType == Transition.next;

		currentRect.anchorMin = NORMAL_MIN_ANCHOR;
		currentRect.anchorMax = NORMAL_MAX_ANCHOR;

		nextRect.anchorMin = NORMAL_MIN_ANCHOR;
		nextRect.anchorMax = NORMAL_MAX_ANCHOR;

		if (next)
		{
			distance = width;
			tranRect = currentRect;

			tranRect.anchorMin = NORMAL_MIN_ANCHOR - Vector2.right;
			tranRect.anchorMax = NORMAL_MAX_ANCHOR - Vector2.right;
		}
		else
		{
			distance = -width;
			tranRect = nextRect;
		}

//		Vector2 max_offset = new Vector2 (distance, -BOARDER);
//		Vector2 min_offset = new Vector2 (distance, BOARDER);
//
//		offsetMax = max_offset;
//		offsetMin = min_offset;

		MoveNextA();

		StartCoroutine(PerformTransitionA());
	}

	IEnumerator PerformTransitionA ()
	{
		transitioning = true;

		while (progress < 1)
		{
			yield return new WaitForEndOfFrame();
			MoveNextA();
		}

		currentCanvas = nextCanvas;
		currentRect = nextRect;
		transitioning = false;
	}

	void MoveNextA ()
	{
		progress = Mathf.Clamp(progress + Time.deltaTime, 0, 1);
		x_offset = Mathf.Lerp (distance, 0, progress / transitionDuration);

		offsetMax = new Vector2 (x_offset - BOARDER, -BOARDER);
		offsetMin = new Vector2 (x_offset + BOARDER, BOARDER);

		tranRect.offsetMax = offsetMax;
		tranRect.offsetMin = offsetMin;
	}


	// Transition as if rotating through
	IEnumerator PerformTransitionB ()
	{
		transitioning = true;

		while (progress < 1)
		{
			progress = Mathf.Clamp(progress + Time.deltaTime, 0, 1);
			x_offset = Mathf.Lerp (distance, 0, progress / transitionDuration);

			offsetMax = new Vector2 (x_offset - BOARDER, -BOARDER);
			offsetMin = new Vector2 (x_offset + BOARDER, BOARDER);

			ApplyRectOffsetsB();

			yield return new WaitForEndOfFrame();
		}

		currentCanvas = nextCanvas;
		currentRect = nextRect;
		transitioning = false;
	}

	void InitializeTransitionB (Transition transitionType, Canvas canvas)
	{
		if (transitioning) { return; }

		nextCanvas = canvas;
		nextRect = nextCanvas.GetComponent<RectTransform>();

		progress = 0;

		var width = currentRect.rect.width;
		bool next = transitionType == Transition.next;
		distance = next ? +width : -width;

		Vector2 max_offset = new Vector2 (distance, -BOARDER);
		Vector2 min_offset = new Vector2 (distance, BOARDER);
		Vector2 anchor_offset = next ? Vector2.left : Vector2.right;

		currentRect.anchorMin = NORMAL_MIN_ANCHOR + anchor_offset;
		currentRect.anchorMax = NORMAL_MAX_ANCHOR + anchor_offset;

		nextRect.anchorMin = NORMAL_MIN_ANCHOR;
		nextRect.anchorMax = NORMAL_MAX_ANCHOR;

		offsetMax = max_offset;
		offsetMin = min_offset;

		ApplyRectOffsetsB();

		StartCoroutine(PerformTransitionB());
	}

	void ApplyRectOffsetsB ()
	{
		currentRect.offsetMax = offsetMax;
		currentRect.offsetMin = offsetMin;

		nextRect.offsetMax = offsetMax;
		nextRect.offsetMin = offsetMin;
	}
}