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

        public override bool Equals(object obj)
        {
            var rhs = (Tile)obj;
            return Position == rhs.Position;
        }
	}
}

