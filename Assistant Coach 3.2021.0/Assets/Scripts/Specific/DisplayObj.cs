using UnityEngine;

public class DisplayObj : MonoBehaviour
{
	public void RemoveBlock()
	{
		FindObjectOfType<CurriculumSelection>().RemoveBlock(gameObject);
		Destroy(gameObject);
	}
}
