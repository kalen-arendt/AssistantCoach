using System.Collections.Generic;

using UnityEngine;

namespace AssistantCoach
{
	public class LocalPageManager : MonoBehaviour, ILocalPageManager
	{
		[SerializeField] private List<Page> pageList;

		private IPage currentPage = null;
		private int currentPageIndex = 0;

		private static ILocalPageManager Instance { get; set; }

		private void Awake()
		{
			if (pageList.Count == 0)
			{
				Debug.LogWarning(
					$"{nameof(LocalPageManager)} {nameof(pageList)} is empty." +
					$"{nameof(LocalPageManager)} will now be destroyed."
				);
				Destroy(this);
			}
			else
			{
				Instance = this;
				PageManagerSingleton.CurrentPageManager = this;
			}
		}

		private void OnDestroy()
		{
			PageManagerSingleton.CurrentPageManager = null;
		}

		private void Start()
		{
			InitializePages();
			//Instance.SwitchToPage(0);
		}

		public void InitializePages()
		{
			foreach (IPage p in pageList)
			{
				p.Initialize();
			}

			currentPage = pageList[currentPageIndex = 0];
			currentPage.Open();

			//for( int i = 1; i < pageList.Count; i++ ) {
			//	pageList[i].CloseSilently();
			//}
		}


		void ILocalPageManager.PageNext()
		{
			try
			{
				//((IPageManager)this).SwitchToPage(pageList[++currentPageIndex]);
				((ILocalPageManager)this).SwitchToPage(currentPageIndex + 1);
			}
			catch
			{
				//currentPageIndex--;
				Debug.LogWarning($"Page at index {currentPageIndex} has no next Page.", this);
			}
		}

		void ILocalPageManager.PagePrevious()
		{
			try
			{
				((ILocalPageManager)this).SwitchToPage(currentPageIndex - 1);
			}
			catch
			{
				//currentPageIndex++;
				Debug.LogWarning($"Page at index {currentPageIndex} has no previous Page.", this);
			}
		}

		void ILocalPageManager.SwitchToPage(int index)
		{
			IPage page = pageList[index];

			page.Open();

			currentPage?.Close();

			currentPage = page;
			currentPageIndex = index;

			//currentPage?.Open();
		}

		//void IPageManager.SwitchToPage (Page page)
		//{
		//	if( page == null ) {
		//		Debug.LogWarning($"the Page passed to {nameof(IPageManager.SwitchToPage)} cannot be null!");
		//		return;
		//	}

		//	page.Open();

		//	foreach( Page p in pageList ) {
		//		if (p != page) {
		//			p.Close();
		//		}
		//	}
		//}
	}
}