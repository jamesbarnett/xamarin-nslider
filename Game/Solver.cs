using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Game
{
	public class Solver
	{
		public Puzzle Game { get; set; }
		public Solver(Puzzle game)
		{
			Game = game;
		}
		
		public List<Move> Solution()
		{
			var root = new SolutionNode(null, Game.Current);
			if (Game.Current.Equals(Game.Solved))
			{
				var moves = new List<Move>();
				moves.Add(new Move(Move.Moves.Start));
				return moves;
			}

			var legalMoves = Game.Current.LegalMoves();
			var visited = new List<Tuple<SolutionNode, Move>>();

			visited.Add(new Tuple<SolutionNode, Move>(root,
				new Move(Move.Moves.Start)));

			var nodeQueue = new Queue<Tuple<SolutionNode, Move>>();

			foreach (var move in legalMoves)
			{
				var board = new Board(Game.Current);
				board.ApplyMove(move);

				var node = new SolutionNode(root, board);
				root.Children.Add(move, node);

				if (board.Equals(Game.Solved))
				{
					var n = new Tuple<SolutionNode, Move>(node, move);
					visited.Add(n);
					return SolutionPath(n, visited);
				}
				else
				{
					nodeQueue.Enqueue(
						new Tuple<SolutionNode, Move>(node, move));
				}
			}

			return BreadthFirstSearch(nodeQueue, visited);
		}
		
		public List<Move> BreadthFirstSearch(
			Queue<Tuple<SolutionNode, Move>> nodeQueue,
			List<Tuple<SolutionNode, Move>> visited
		)
		{
			while (nodeQueue.Count > 0)
			{
				Tuple<SolutionNode, Move> node = nodeQueue.Dequeue ();
				
				if (node.Item1.Current.Equals(Game.Solved))
				{
					visited.Add(node);
					return SolutionPath(node, visited);
				}
			}
			
			return new List<Move>();
		}
		
		public List<Move> SolutionPath(Tuple<SolutionNode, Move> solution,
		                               List<Tuple<SolutionNode, Move>> visited)
		{
			var moves = new List<Move>();
			var node = new Tuple<SolutionNode, Move>(solution.Item1, solution.Item2);
			
			moves.Add(solution.Item2);
			
			while (node.Item1.Parent != null)
            {
				Tuple<SolutionNode, Move> nextChild = null;
				
				for (int i = 0; i < visited.Count; ++i) {
					nextChild = visited [i];
					
					if (nextChild.Item1.Equals(node.Item1.Parent)) {
						node = nextChild;
						break;
					}
				}
				
				// node = visited.Find(m => m.Item1 == node.Item1.Parent);
				
				if (node != null) {
					moves.Add(node.Item2);
				} else {
					throw new Exception("Didn't find node");
				}
			}
			
			moves.Reverse();
			
			return moves;
		}
	}
}

