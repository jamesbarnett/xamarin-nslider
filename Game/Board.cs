using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Game
{
	public class Board
	{
		private List<Tile> _grid = null;
        public int Order { get; set; }
        public List<Tile> Grid
        {
            get { return _grid; }
            set { _grid = value; }
        }

		public Board(int order)
		{
			Order = order;

			_grid = new List<Tile>();

			for (int i = 0; i < Order * Order; i++)
			{
				_grid.Add(new Tile(i));
			}
		}

		public Board(Board src)
		{
			Order = src.Order;
			_grid = new List<Tile>();

			foreach (var tile in src._grid)
			{
				_grid.Add(new Tile(tile));
			}
		}

		public IEnumerable<Move> Shuffle(int count)
		{
			var moves = new List<Move>();
			var gameStates = new List<Board>();
			var legalMoves = new List<Move>(LegalMoves());
			ShuffleMoves(legalMoves);
			Move moveToApply = null;
			
			gameStates.Add(this);
			
			for (int i = 0; i < count; i++) 
			{
				foreach (var move in legalMoves) 
				{
					Board board = new Board(this);
					board.ApplyMove(move);
					
					bool hasBoard = false;

					foreach (var gameState in gameStates)
					{
						if (gameState.IsSameBoard(board))
						{
							hasBoard = true;
							break;
						}
					}

					if (hasBoard)
					{
						gameStates.Add(board);
						moveToApply = move;
						break;
					}
				}
				
				ApplyMove(moveToApply);
				moves.Add(moveToApply);
			}
			
			return moves;
		}

		public void ApplyMove(Move move)
		{
			int pos = FindEmptyTile();
		
			// Debug.WriteLine(string.Format("ApplyMove: move is {0}", move.ToString()));
			switch (move.Direction) 
			{
			case Move.Moves.Up:
				SwapTiles(pos - Order, pos);
				break;
				
			case Move.Moves.Left:
				SwapTiles(pos - 1, pos);
				break;
				
			case Move.Moves.Down:
				SwapTiles(pos + Order, pos);
				break;
				
			case Move.Moves.Right:
				SwapTiles(pos + 1, pos);
				break;
				
			case Move.Moves.Start:
			case Move.Moves.Solved:
				break;
			
			}
		}

		public int FindEmptyTile()
		{
			/* This code should work, appears to be a bug in Xamarin Studio
			return _grid.FindIndex(x => x.Position == 0);
			*/
			for (int i = 0; i < Order * Order; i++)
			{
				if (_grid[i].Position == 0)
					return i;
			}
			
			return -1;
		}
		
		public IEnumerable<Move> LegalMoves()
		{
			var legal = new List<Move>();
		
			int pos = FindEmptyTile();
			
			if (pos >= Order) legal.Add(new Move(Move.Moves.Up));
			if (pos % Order != 0) legal.Add(new Move(Move.Moves.Left));
			if (pos % Order != Order - 1) legal.Add(new Move(Move.Moves.Right));
			if (pos < Order * Order - Order) legal.Add(new Move(Move.Moves.Down));
			
			return legal;
		}

		public bool IsSameBoard(Board rhs)
        {
            if (Order == rhs.Order)
            {
                for (int i = 0; i < Grid.Count; i++)
                {
					if (!Grid[i].IsSameTile(rhs.Grid[i])) return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
		
		public override string ToString()
		{
			var buffer = new StringBuilder();
			buffer.AppendFormat("Board -- Order: {0}\n", Order);

			return buffer.ToString();
		}

		private void SwapTiles(int origin, int destination)
		{
			Tile temp = _grid[origin];
			_grid[origin] = _grid[destination];
			_grid[origin] = temp;
		}
		
		private void ShuffleMoves(List<Move> moves)
		{
			Random rng = new Random ();
			int n = moves.Count;
			
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				Move m = moves[k];
				moves[k] = moves[n];
				moves[n] = m;
			}
		}
	}
}

