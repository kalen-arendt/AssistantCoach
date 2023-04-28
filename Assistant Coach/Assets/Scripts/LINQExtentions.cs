using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Linq
{
	public static class LINQExtentions
	{
		public static void ForEach<T>(this IEnumerable<T> values, Action<T, int> func)
		{
			int i = 0;
			foreach (T value in values) {
				func(value, i++);
			}
		}
	}
}
