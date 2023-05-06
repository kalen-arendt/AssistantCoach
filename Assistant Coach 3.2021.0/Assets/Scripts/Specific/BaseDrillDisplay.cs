using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class BaseDrillDisplay : MonoBehaviour
{
	[SerializeField] private Image diagram = null;
	[SerializeField] private Text description = null;
	[SerializeField] private GameObject SubLayoutController = null;

	[SerializeField] private Text block = null;
	[SerializeField] private Text topic = null;
	[SerializeField] private Text time = null;

	[SerializeField] private Slider fontSizeSlider = null;
	[SerializeField] private int startingFontSize = 25;

	//private ContentSizeFitter contentSizeFitter;
	//private Canvas parentCanvas;
	//	ApplicationController app;


	protected BlockTopic Subject { get; set; }

	private int fontSize;
	// UNUSED
	//private const int ROOT_FONT_SIZE = 45;
	private const int SPACING = 2;


	public bool Portrait { get; set; }

	private void OnEnable()
	{
		fontSize = startingFontSize;
		fontSizeSlider.value = fontSize;
		//		fontSizeSlider.onValueChanged += ChangeTextSize(fontSizeSlider.value);

		//		app = FindObjectOfType<ApplicationController> ();
		//parentCanvas = GetComponentInParent<Canvas> ();
	}


	// TODO fix whatever this is...
	//	int c = 21;
	//	int t = 20;
	//	void LateUpdate ()
	//	{
	//		c++;
	//
	//		if (c > t)
	//		{
	//			if (app.CurrentCanvas != parentCanvas)
	//			{
	//				t = 50;
	//			}
	//			else
	//			{
	//				ResizeAllElementsForBestFit ();
	//				t = 20;
	//			}
	//
	//			c = 0;
	//		}
	//	}

	public void ShowDrillBaseRootIndex(BlockOutputData _block, int index, int _time, BlockTopic subject)
	{
		diagram.sprite = _block.mySprite;
		time.text = _time + "min";
		block.text = "Block " + (index + 1);
		topic.text = subject.AsString();

		ResizeImageToMaxSize(diagram);

		description.text = _block.descriptionText.Trim() + "\n";
		description.fontSize = fontSize;
	}


	public void ToggleLayoutType()
	{
		StopAllCoroutines();
		StartCoroutine(EnumerateToggleLayoutType());
	}

	private IEnumerator EnumerateToggleLayoutType()
	{
		//Portrait(stacked) => Horizontal (side-by-side)
		if (Portrait)
		{
			Portrait = !Portrait;
			if (SubLayoutController.TryGetComponent<VerticalLayoutGroup>(out var prev))
			{
				Destroy(prev);
			}

			yield return new WaitForEndOfFrame();

			HorizontalLayoutGroup nLayout = SubLayoutController.AddComponent<HorizontalLayoutGroup> ();

			nLayout.spacing = SPACING;
			nLayout.padding.left = SPACING;
			nLayout.padding.right = SPACING;

			nLayout.childControlHeight = false;

			description.fontSize = fontSize;
		}
		else
		{
			Portrait = !Portrait;

			if (SubLayoutController.TryGetComponent<HorizontalLayoutGroup>(out var prev))
			{
				Destroy(prev);
			}

			yield return new WaitForEndOfFrame();

			VerticalLayoutGroup nLayout = SubLayoutController.AddComponent<VerticalLayoutGroup> ();

			nLayout.spacing = SPACING;
			nLayout.padding.top = SPACING;
			nLayout.padding.bottom = SPACING;

			nLayout.childControlHeight = false;

		}


		ResizeAllElementsForBestFit();
		SubLayoutController.GetComponent<HorizontalOrVerticalLayoutGroup>().SetLayoutVertical();
		GetComponent<HorizontalOrVerticalLayoutGroup>().CalculateLayoutInputVertical();
		GetComponent<HorizontalOrVerticalLayoutGroup>().SetLayoutVertical();


		yield return null;
	}



	//	void ToggleDrillType (BlockStruct.Description_Type _type)
	//	{
	//		if (_type == type) return;
	//
	//		if (_type == BlockStruct.Description_Type.graphic)
	//		{
	//			DestroyImmediate (description.GetComponent<Text>());
	//			image = description.AddComponent<Image>();
	//			description.GetComponent<ChildContentFitter> ().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
	//		}
	//		else 
	//		{
	//			DestroyImmediate (description.GetComponent<Image>());
	//			text = description.AddComponent<Text>();
	//			description.GetComponent<ChildContentFitter> ().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
	//
	//			text.color = Color.black;
	//			text.font = ApplicationController.instance.DescriptionFont;
	//			text.fontSize = fontSize;
	//		}
	//
	//		type = _type;
	//	}



	public void ResizeAllElementsForBestFit()
	{
		ResizeImageToMaxSize(diagram);

		if (description.TryGetComponent<Image>(out var descriptImage))
		{
			ResizeImageToMaxSize(descriptImage);
		}
	}

	private void ResizeImageToMaxSize(Image image)
	{
		if (image.sprite == null)
		{
			return;
		}

		image.preserveAspect = true;

		Canvas.ForceUpdateCanvases();

		RectTransform rt = image.GetComponent<RectTransform> ();

		var width = rt.rect.size.x;
		var newHeight =  width * image.sprite.texture.height / image.sprite.texture.width;

		var nV2 = new Vector2 (0, newHeight);

		if (nV2 != rt.sizeDelta)
		{
			rt.sizeDelta = new Vector2(0, newHeight);
		}
	}


	public void ToggleObjectActive(GameObject obj)
	{
		var active = obj.activeSelf == false;
		obj.SetActive(active);
	}


	public void ChangeTextSize(float value)
	{
		fontSize = (int)value;
		description.fontSize = fontSize;
	}
}
