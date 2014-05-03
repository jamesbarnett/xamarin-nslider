using System;

namespace Game
{
	public class Tile
	{
		public int Position { get; set; }

		public Tile(int position)
		{
			Position = position;
		}

		public Tile(Tile src)
		{
			this.Position = src.Position;
		}
	}
}

