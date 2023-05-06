using UnityEngine;
public readonly struct Date
{
	public readonly int Day, Month, Year;

	public Date(int day, int month, int year)
	{
		Day = Mathf.Clamp(day, 1, 31);
		Month = Mathf.Clamp(month, 1, 12);
		Year = Mathf.Clamp(year, 2018, 2019);
	}

	public string ShortDate()
	{
		return string.Format("{0}/{1}", Month, Day);
	}

	public string LongDate()
	{
		return string.Format("{0}/{1}/{2}", Month, Day, Year - 2000);
	}
}