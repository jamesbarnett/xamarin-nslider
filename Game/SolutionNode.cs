using System;
using System.Collections.Generic;


namespace Game
{
	public class SolutionNode
	{
		private SolutionNode _parent = null;
		private Board _current = null;
		private Dictionary<Move, SolutionNode> _children = new Dictionary<Move, SolutionNode>();
		
		public Board Current
        { 
			get { return _current; }
			set { _current = value; }
		}
		
		public SolutionNode Parent
        {
			get { return _parent; }
			set { _parent = value; }
		}

		public Dictionary<Move, SolutionNode> Children
        {
			get { return _children; }
			set { _children = value; }
		}

		public SolutionNode(SolutionNode parent, Board board)
		{
			_parent = parent;
			_current = board;
		}
	}
}

