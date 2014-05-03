using NUnit.Framework;
using System;
using Game;

namespace Test
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			var game = new Puzzle(3);
			var solver = new Solver(game);
			var solution = solver.Solution();
			
			foreach (var step in solution) {
				Console.WriteLine(string.Format("Step is #{0}", step));
			}
		}
	}
}

