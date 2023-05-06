using UnityEngine;
using UnityEngine.UI;

public class AgeDateManager : MonoBehaviour
{
	[Header ("Age")]
	[SerializeField] private Slider ageSlider = null;
	[SerializeField] private Text ageOutput = null;

	[Header ("Date")]
	[SerializeField] private Slider daySlider = null;
	[SerializeField] private Slider monthSlider = null;
	[SerializeField] private Slider yearSlider = null;

	[Space]
	[SerializeField] private Text dayOutput = null;
	[SerializeField] private Text monthOutput = null;
	[SerializeField] private Text yearOutput = null;

	public int Age { get; private set; }
	public int Day { get; private set; }
	public int Month { get; private set; }
	public int Year { get; private set; }

	public Date Date => new(Day, Month, Year);

	private void OnEnable()
	{
		ageSlider.onValueChanged.AddListener(
			delegate (float value)
			{ OnAgeValueChanged((int)value); }
		);

		daySlider.onValueChanged.AddListener(
			delegate (float value)
			{ OnDayValueChanged((int)value); }
		);

		monthSlider.onValueChanged.AddListener(
			delegate (float value)
			{ OnMonthValueChanged((int)value); }
		);

		yearSlider.onValueChanged.AddListener(
			delegate (float value)
			{ OnYearValueChanged((int)value); }
		);

		GetPrefDate();
	}

	private void GetPrefDate()
	{
		Day = PlayerPrefsManager.Settings.Day;
		Month = PlayerPrefsManager.Settings.Month;
		Year = PlayerPrefsManager.Settings.Year;
		Age = PlayerPrefsManager.Settings.Age;

		daySlider.value = Day;
		monthSlider.value = Month;
		yearSlider.value = Year;
		ageSlider.value = Age;

		OnAgeValueChanged(Age);
		OnDayValueChanged(Day);
		OnMonthValueChanged(Month);
		OnYearValueChanged(Year);
	}

	private void OnAgeValueChanged(int value)
	{
		Age = value;
		ageOutput.text = "U" + Age;
		PlayerPrefsManager.Settings.Age = Age;
	}

	private void OnDayValueChanged(int value)
	{
		Day = value;
		dayOutput.text = Day.ToString();
		PlayerPrefsManager.Settings.Day = Day;
	}

	private void OnMonthValueChanged(int value)
	{
		Month = value;
		monthOutput.text = Month.ToString();
		PlayerPrefsManager.Settings.Month = Month;
	}

	private void OnYearValueChanged(int value)
	{
		Year = value;
		yearOutput.text = Year.ToString();
		PlayerPrefsManager.Settings.Year = Year;
	}
}
