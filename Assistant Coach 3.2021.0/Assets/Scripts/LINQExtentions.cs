using System.Collections.Generic;

namespace System.Linq
{
	public static class LINQExtentions
	{
		public static void ForEach<T>(this IEnumerable<T> values, Action<T, int> func)
		{
			var i = 0;
			foreach (T value in values)
			{
				func(value, i++);
			}
		}
	}
}
