using System;
using System.Collections.Generic;

namespace Game
{
	public class Board
	{
		private int _order;
		private List<Tile> _grid = null;

		public Board(int order)
		{
			_order = order;

			_grid = new List<Tile>();

			for (int i = 0; i < _order; i++)
			{
				_grid.Add(new Tile(i));
			}
		}

		public Board(Board src)
		{
			_order = src._order;
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
			
			gameStates.Add (this);
			
			for (int i = 0; i < count; i++) 
			{
				foreach (var move in legalMoves) 
				{
					Board board = (Board)this.MemberwiseClone ();
					board.ApplyMove (move);
					
					if (!gameStates.Contains (board))
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
			
			switch (move.Direction) 
			{
			case Move.Moves.Up:
				SwapTiles(pos - _order, pos);
				break;
				
			case Move.Moves.Left:
				SwapTiles(pos - 1, pos);
				break;
				
			case Move.Moves.Down:
				SwapTiles(pos + _order, pos);
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
			for (int i = 0; i < _order * _order; i++)
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
			
			if (pos >= _order) legal.Add(new Move(Move.Moves.Up));
			if (pos % _order != 0) legal.Add(new Move(Move.Moves.Left));
			if (pos % _order != _order - 1) legal.Add(new Move(Move.Moves.Right));
			if (pos < _order * _order - _order) legal.Add(new Move(Move.Moves.Down));
			
			return legal;
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

