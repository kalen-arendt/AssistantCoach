using System.Collections.Generic;
using System.Linq;

public enum BlockTopic
{
	SA,
	PL,
	BS,
	D,
	R,
	PR,
	SSG,
	PwT,
	PwG,
	VA,
	H,
	_1v1D
}

public static class BlockTopicListExtensions
{
	public static string Summarize(this IEnumerable<BlockTopic> topics)
	{
		return string.Join("-", topics.Select(topic => topic.AsString()));
	}

	public static string AsString(this BlockTopic subject)
	{
		return subject switch
		{
			BlockTopic.PR => "P+R",
			BlockTopic.PwG => "Pw/G",
			BlockTopic.PwT => "Pw/T",
			BlockTopic.VA => "V+A",
			BlockTopic._1v1D => "1v1D",
			_ => subject.ToString(),
		};
	}
}