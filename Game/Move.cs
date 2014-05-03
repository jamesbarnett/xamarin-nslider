using System;

namespace Game
{
	public class Move
	{
		public enum Moves { Up = 1, Left, Down, Right, Start, Solved };

		public Moves Direction { get; set; }
		public Move(Move.Moves direction)
		{
			Direction = direction;
		}
	}
}

