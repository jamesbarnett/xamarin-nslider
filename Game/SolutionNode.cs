using System;
using System.Collections.Generic;


namespace Game
{
	public class SolutionNode
	{
		private SolutionNode _parent = null;
		private Board _board = null;
		private Dictionary<Move, Board> _children = new Dictionary<Move, Board>();
		
		public Board Current { 
			get { return _current; }
			set { _current = value; }
		}
		
		public SolutionNode Parent {
			get { return _parent; }
			set { _parent = value; }
		}
		
		public SolutionNode(SolutionNode parent, Board board)
		{
			_parent = parent;
			_board = board;
		}
		
		public bool IsBoardMatch(Board b)
		{
			return _board.Equals (b);
		}
	}
}

