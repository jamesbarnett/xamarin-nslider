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

		public bool IsSameMove(Move rhs)
        {
            return Direction == rhs.Direction;
        }

		public override string ToString()
		{
			var str = "";

			switch (Direction)
			{
			case Moves.Up:
				str = "UP";
				break;
			case Moves.Left:
				str = "LEFT";
				break;
			case Moves.Down:
				str = "DOWN";
				break;
			case Moves.Right:
				str = "RIGHT";
				break;
			case Moves.Start:
				str = "START";
				break;
			case Moves.Solved:
				str = "SOLVED";
				break;
			default:
				str = "UNKNOWN MOVE!";
				break;
			}

			return str;
		}
	}
}

