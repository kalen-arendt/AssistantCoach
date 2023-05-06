using System.Linq;

public readonly struct SaveObject
{
	public Date Date { get; }
	public int Age { get; }
	public int TotalTime { get; }
	public SubjectData[] SubjectDataArray { get; }

	public SaveObject(SubjectData[] subjectDataArray, Date date, int age, int totalTime)
	{
		SubjectDataArray = subjectDataArray;
		Date = date;
		Age = age;
		TotalTime = totalTime;
	}

	public override string ToString()
	{
		return string.Format("[SaveObject: TopicSummary={0}, TotalTime={1}]", GetTopicSummary(), TotalTime);
	}

	public string GetTopicSummary()
	{
		return SubjectDataArray.Select(subject => subject.subject).Summarize();
	}

	public string GetAge()
	{
		return "u" + Age;
	}
}

public readonly struct SessionData
{
	public readonly Date date;
	public readonly int index;
	public readonly string summary;

	public SessionData(int index, Date date, string summary)
	{
		this.date = date;
		this.index = index;
		this.summary = summary;
	}

	public override string ToString()
	{
		return string.Format("Session {0} [{1}], created {2}", index, summary, date);
	}
}

public readonly struct SubjectData
{
	public readonly int dataBaseIndex;
	public readonly int time;
	public readonly BlockTopic subject;

	public SubjectData(int dataBaseIndex, int time, BlockTopic subject)
	{
		this.time = time;
		this.subject = subject;
		this.dataBaseIndex = dataBaseIndex;
	}

	public override string ToString()
	{
		return string.Format("{0} @ index {1}", subject, dataBaseIndex);
	}
}