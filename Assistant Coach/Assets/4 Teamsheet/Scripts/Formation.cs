using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu (menuName = "Assistant Coach/Formation")]
public class Formation : ScriptableObject
{
	[SerializeField] string formation = "4-4-2";
	List<Position> keeper = new List<Position> () {new Position(position.GK, Vector2.zero)};
	[SerializeField] List<Position> forwards = new List<Position> ();
	[SerializeField] List<Position> midfielders = new List<Position> ();
	[SerializeField] List<Position> defenders = new List<Position> ();

	[Header ("Spacing")]
	[SerializeField] float forwardSpacing = 120;
	[SerializeField] float midfieldSpacing = 100;
	[SerializeField] float defenceSpacing = 120;


	public string Name () { return formation; }
	public List<Position> Keeper { get { return keeper; }}
	public List<Position> Forwards { get { return forwards; }}
	public List<Position> Midfielders { get { return midfielders; }}
	public List<Position> Defenders { get { return defenders; }}

	public float DefenceSpacing { get { return defenceSpacing; }}
	public float MidfieldSpacing { get { return midfieldSpacing; }}
	public float ForwardSpacing { get { return forwardSpacing; }}

	public enum position {RB, LB, CB, RWB, LWB, RM, LM, CM, CAM, CDM, RW, LW, ST, CF, GK}


	[System.Serializable]
	public struct Position
	{
		[SerializeField] position role;
		[SerializeField] Vector2 offset;


		public Position (position position, Vector2 offset)
		{
			this.role = position;
			this.offset = offset;
		}

		public position Role { get { return role; }}
		public Vector2 Offset { get { return offset; }}
		public Color Color {
			get
			{
				int index = (int)role;
				if (index < 5) {
					return Color.green;
				} else if (index < 10) {
					return Color.cyan;
				} else if (index < 14) {
					return Color.magenta;
				} else {
					return Color.yellow;
				}
			}
		}
	}
}
