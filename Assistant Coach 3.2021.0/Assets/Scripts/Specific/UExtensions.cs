using System;

using UnityEngine;

public static class UExtensions
{
	public static T CheckNull<T>(this T t)
		where T : MonoBehaviour
	{
		return t is null ? null : t;
	}

	public static void IfThenElse(this bool condition, Action funcIf, Action funcElse)
	{
		if (condition)
		{
			funcIf();
		}
		else
		{
			funcElse();
		}
	}
}
