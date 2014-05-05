using System;

namespace Game
{
	public class Tile : Object
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

		public bool IsSameTile(Tile rhs)
        {
            return Position == rhs.Position;
        }
	}
}

