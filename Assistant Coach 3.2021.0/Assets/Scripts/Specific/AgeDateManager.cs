using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgeDateManager : MonoBehaviour
{
	[Header ("Age")]
	[SerializeField] Slider ageSlider = null;
	[SerializeField] Text ageOutput = null;

	[Header ("Date")]
	[SerializeField] Slider daySlider = null;
	[SerializeField] Slider monthSlider = null;
	[SerializeField] Slider yearSlider = null;

	[Space]
	[SerializeField] Text dayOutput = null;
	[SerializeField] Text monthOutput = null;
	[SerializeField] Text yearOutput = null;

	public int Age{get; private set;}
	public int Day{get; private set;}
	public int Month {get; private set;}
	public int Year {get; private set;}

	public Date Date { get { return new Date (Day, Month, Year);}}

	void OnEnable ()
	{
		ageSlider.onValueChanged.AddListener(
			delegate (float value) { OnAgeValueChanged ((int)value); }
		);

		daySlider.onValueChanged.AddListener(
			delegate (float value) { OnDayValueChanged ((int)value); }
		);

		monthSlider.onValueChanged.AddListener(
			delegate (float value) { OnMonthValueChanged ((int)value); }
		);

		yearSlider.onValueChanged.AddListener(
			delegate (float value) { OnYearValueChanged ((int)value); }
		);

		GetPrefDate ();
	}

	void GetPrefDate ()
	{
		Day= PlayerPrefsManager.Settings.Day;
		Month = PlayerPrefsManager.Settings.Month;
		Year = PlayerPrefsManager.Settings.Year;
		Age= PlayerPrefsManager.Settings.Age;

		daySlider.value = Day;
		monthSlider.value = Month;
		yearSlider.value = Year;
		ageSlider.value = Age;

		OnAgeValueChanged(Age);
		OnDayValueChanged(Day);
		OnMonthValueChanged(Month);
		OnYearValueChanged(Year);
	}

	void OnAgeValueChanged (int value)
	{
		Age = value;
		ageOutput.text = "U" + Age;
		PlayerPrefsManager.Settings.Age = Age;
	}

	void OnDayValueChanged (int value)
	{
		Day = value;
		dayOutput.text = Day.ToString();
		PlayerPrefsManager.Settings.Day = Day;
	}

	void OnMonthValueChanged (int value)
	{
		Month = value;
		monthOutput.text = Month.ToString();
		PlayerPrefsManager.Settings.Month = Month;
	}

	void OnYearValueChanged (int value)
	{
		Year = value;
		yearOutput.text = Year.ToString();
		PlayerPrefsManager.Settings.Year = Year;
	}
}
