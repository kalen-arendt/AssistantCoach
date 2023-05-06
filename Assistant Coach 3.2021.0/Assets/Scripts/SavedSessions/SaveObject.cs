using System.Collections.Generic;

public struct SaveObject
{
	public readonly Date date;
	public readonly int age;
	public readonly int totalTime;
	public readonly SubjectData[] subjectDataArray;

	public SaveObject (SubjectData[] subjectDataArray, Date date, int age, int totalTime)
	{
		this.subjectDataArray = subjectDataArray;
		this.date = date;
		this.age = age;
		this.totalTime = totalTime;
		//timeList = new List<int> (TimeList);
	}

	public override string ToString ()
	{
		return string.Format ("[SaveObject: TopicSummary={0}, TotalTime={1}]", GetTopicSummary(), totalTime);
	}

	public string GetTopicSummary()
	{
		string str = "";
		foreach (var subject in subjectDataArray)
		{
			str += subject.subject + "-";
		}

		return str.Remove(str.Length - 1);
	}

	public string GetAge()
	{
		return "u" + age;
	}
}

public readonly struct SessionData
{
	public readonly Date date;
	public readonly int index;
	public readonly string summary;

	public SessionData (int index, Date date, string summary)
	{
		this.date = date;
		this.index = index;
		this.summary = summary;
	}

	public override string ToString ()
	{
		return string.Format ("Session {0} [{1}], created {2}", index, summary, date);
	}
}

public readonly struct SubjectData 
{
	public readonly int dataBaseIndex;
	public readonly int time;
	public readonly BlockTopic subject;

	public SubjectData (int dataBaseIndex, int time, BlockTopic subject)
	{
		this.time = time;
		this.subject = subject;
		this.dataBaseIndex = dataBaseIndex;
	}

	public override string ToString ()
	{
		return string.Format ("{0} @ index {1}", subject, dataBaseIndex);
	}
}