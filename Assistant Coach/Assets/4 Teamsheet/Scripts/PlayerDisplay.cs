using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
	[SerializeField] Text nameLabel = null;

	Player player;

	public Player Player {
		get { return player; }
		set {
			player = value;
			nameLabel.text = player.Name;
		}
	}


}
