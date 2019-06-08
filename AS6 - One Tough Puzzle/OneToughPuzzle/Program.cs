using System;

namespace OneToughPuzzle
{
	class Program
	{
		static void Main(string[] args)
		{
			Solve();
		}

		static void Solve()
		{
			var _puzzle = new Puzzle();

			
			if (_puzzle.Place())
				Console.Write("ONE TOUGH PUZZLE\n" +
							  "Seed: " + _puzzle.GetSeed + "\nSteps: " + _puzzle.Steps + "\n\n\n\n" + _puzzle.PrintPuzzle() + 
							  "\n\n\n\nSolved by Matthew Jett");
			else
				Console.WriteLine("SURPRISE! Not solvable\n");				
		}
		// DIAGNOSTICS //
		//Console.WriteLine(_puzzle.ToString());

		///// I know my indexing works by coordinate in puzzle board and second index is the TAB in question. Can I do this with Words for clarity?
		//if ((int)_puzzle[0, 0][1] + _puzzle[0, 1][3] == 0)
		//	return;
		//if (_puzzle[PuzzlePiece.PIECE_3][4] == _puzzle[0, 0][2])
		//	return;
		//if (_puzzle[PuzzlePiece.PIECE_0].Rotate().ConnectsTo(_puzzle[0,0]))
		//	return;

		/*Rotation checking print statements work!!*/
		//	Console.WriteLine(string.Format("{0} {1} connects to {2} {3}", puzzle[0,0], puzzle[0,0][1], puzzle[0,1], puzzle[0,1][3]));
		//puzzle[0, 0].Rotate();
		//Console.WriteLine(puzzle[0, 0].ToString());
	}
}
