using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseDrillDisplay : MonoBehaviour
{
	[SerializeField] Image diagram = null;
	[SerializeField] Text description = null;
	[SerializeField] GameObject	SubLayoutController = null;

	[SerializeField] Text block = null;
	[SerializeField] Text topic = null;
	[SerializeField] Text time = null;

	[SerializeField] Slider fontSizeSlider = null;
	[SerializeField] int startingFontSize = 25;

	ContentSizeFitter contentSizeFitter;
	Canvas parentCanvas;
//	ApplicationController app;


	protected BlockTopic subject {get; set;}

	int fontSize;

	const int ROOT_FONT_SIZE = 45;
	const int SPACING = 2;


	public bool Portrait { get; set; }


	void OnEnable ()
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

	public void ShowDrillBaseRootIndex (BlockOutputData _block, int index, int _time, BlockTopic subject)
	{
		diagram.sprite = _block.mySprite;
		time.text = _time + "min";
		block.text = "Block " + (index + 1);
		topic.text = CurriculumSelection.SubjectAsString(subject);

		ResizeImageToMaxSize (diagram);

		description.text = _block.descriptionText.Trim () + "\n";
		description.fontSize = fontSize;
	}


	public void ToggleLayoutType ()
	{
		StopAllCoroutines ();
		StartCoroutine (pToggleLayoutType ());
	}


	IEnumerator pToggleLayoutType ()
	{
		//Portrait(stacked) => Horizontal (side-by-side)
		if (Portrait)
		{
			Portrait = !Portrait;
			var prev = SubLayoutController.GetComponent<VerticalLayoutGroup> ();
			if (prev != null) Destroy (prev);

			yield return new WaitForEndOfFrame ();

			var nLayout = SubLayoutController.AddComponent<HorizontalLayoutGroup> ();

			nLayout.spacing = SPACING;
			nLayout.padding.left = SPACING;
			nLayout.padding.right = SPACING;

			nLayout.childControlHeight = false;

			description.fontSize = fontSize;
		}
		else
		{
			Portrait = !Portrait;

			var prev = SubLayoutController.GetComponent<HorizontalLayoutGroup> ();
			if (prev != null) Destroy (prev);

			yield return new WaitForEndOfFrame ();

			var nLayout = SubLayoutController.AddComponent<VerticalLayoutGroup> ();

			nLayout.spacing = SPACING;
			nLayout.padding.top = SPACING;
			nLayout.padding.bottom = SPACING;

			nLayout.childControlHeight = false;

		}


		ResizeAllElementsForBestFit ();
		SubLayoutController.GetComponent<HorizontalOrVerticalLayoutGroup>().SetLayoutVertical ();
		this.GetComponent<HorizontalOrVerticalLayoutGroup>().CalculateLayoutInputVertical ();
		this.GetComponent<HorizontalOrVerticalLayoutGroup>().SetLayoutVertical ();


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



	public void ResizeAllElementsForBestFit ()
	{
		ResizeImageToMaxSize (diagram);

		var descriptImage = description.GetComponent<Image> ();

		if (descriptImage != null)
			ResizeImageToMaxSize (descriptImage);
	}


	void ResizeImageToMaxSize (Image _image)
	{
		if (_image.sprite == null) return;
		_image.preserveAspect = true;

		Canvas.ForceUpdateCanvases ();

		RectTransform rt = _image.GetComponent<RectTransform> ();

		float width = rt.rect.size.x;
		float newHeight =  width * (float)_image.sprite.texture.height / (float)_image.sprite.texture.width;

		Vector2 nV2 = new Vector2 (0, newHeight);

		if (nV2 != rt.sizeDelta)
			rt.sizeDelta = new Vector2 (0, newHeight);
	}


	public void ToggleObjectActive (GameObject obj)
	{
		bool active = obj.activeSelf == false;
		obj.SetActive (active);
	}


	public void ChangeTextSize (float value)
	{
		fontSize = (int)value;
		description.fontSize = fontSize;
	}
}
