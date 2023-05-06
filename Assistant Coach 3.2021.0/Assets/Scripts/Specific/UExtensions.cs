using UnityEngine;

public static class UExtensions
{
	public static T CheckNull<T>(this T t)
		where T : MonoBehaviour
	{
		return t == null ? null : t;
	}
}
