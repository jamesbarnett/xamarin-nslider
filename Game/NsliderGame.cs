using System;

namespace Game
{
	public class NsliderGame
	{
		public Board Current { get; set; }
		public Board Solved { get; set; }
		public int Order { get; set; }
		
		public NsliderGame(int order, int shuffleMoves = 10)
		{
			Current = Solved = new Board(order);
			Order = order;
			Current.Shuffle(shuffleMoves);
		}
	}
}

