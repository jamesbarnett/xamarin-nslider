using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Game
{
	public class Solver
	{
		public NsliderGame Game { get; set; }
		public Solver(NsliderGame game)
		{
			Game = game;
		}
		
		public List<Move> Solution()
		{
			return new List<Move>();
		}
		
		public List<Move> BreadFirstSearch (
			Queue<Tuple<SolutionNode, Move>> nodeQueue,
			List<Tuple<SolutionNode, Move>> visited)
		{
			while (nodeQueue.Count > 0)
			{
				Tuple<SolutionNode, Move> node = nodeQueue.Dequeue ();
				
				if (node.Item1.Current == Game.Solved)
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
			
			moves.Add (solution.Item2);
			
			while (node.Item1.Parent != null) {
				node = visited.Find(m => m.Item1 == node.Item1.Parent);
				
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

