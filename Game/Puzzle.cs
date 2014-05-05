using System;

namespace Game
{
	public class Puzzle
	{
		public Board Current { get; set; }
		public Board Solved { get; set; }
		
		public Puzzle(int order, int shuffleMoves = 10)
		{
			Solved = new Board(order);
			Current = new Board(Solved);
			Current.Shuffle(shuffleMoves);
		}
	}
}

