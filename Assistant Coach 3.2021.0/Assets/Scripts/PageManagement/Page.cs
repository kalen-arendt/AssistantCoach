using System;

using UnityEngine;

namespace AssistantCoach
{
	public class Page : MonoBehaviour, IPage
	{
		private const int OPEN_PAGE_SORT_ORDER = 100;
		private const int CLOSED_PAGE_SORT_ORDER = 0;

		private Canvas myCanvas;

		public event Action<Page> OnSubmit;
		public event Action<Page> OnPageInitialized;
		public event Action <Page> OnPageOpened;
		public event Action <Page> OnPageClosed;


		void IPage.Initialize()
		{
			myCanvas = GetComponent<Canvas>();
			myCanvas.sortingOrder = CLOSED_PAGE_SORT_ORDER;

			RectTransform rect = GetComponent<RectTransform>();
			rect.anchorMin = Vector2.zero;
			rect.anchorMax = Vector2.one;

			gameObject.SetActive(false);

			OnPageInitialized?.Invoke(this);
		}

		//public void OpenSilently ()
		//{
		//	myCanvas.sortingOrder = OPEN_PAGE_SORT_ORDER;
		//	gameObject.SetActive(true);
		//}

		//public void CloseSilently ()
		//{
		//	myCanvas.sortingOrder = CLOSED_PAGE_SORT_ORDER;
		//	gameObject.SetActive(false);
		//}


		public void Open()
		{
			myCanvas.sortingOrder = OPEN_PAGE_SORT_ORDER;
			gameObject.SetActive(true);
			OnPageOpened?.Invoke(this);
		}

		public void Close()
		{
			myCanvas.sortingOrder = CLOSED_PAGE_SORT_ORDER;
			gameObject.SetActive(false);
			OnPageClosed?.Invoke(this);
		}

		public void Submit()
		{
			Debug.Log("Submitted!");
			OnSubmit?.Invoke(this);
		}
	}
}