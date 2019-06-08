using System;
using System.Collections.Generic;
using System.Text;

namespace OneToughPuzzle
{
	internal class Medial : PuzzleLocation
	{
		// FIELDS

		// PROPERTIES
		internal LOCATION Name { get; }
		internal override PuzzlePiece[] Position { get => Array<PuzzlePiece>.GetRow(_board, 1); set => Top_Edge = value; }
		//internal PuzzlePiece[] Medial { get => Array<PuzzlePiece>.GetRow(_board, 1); set => Top_Edge = value; }
		// TODO: Tie this in as an abstract property and access it through the PuzzleLocation.SetPiece() method for the location property
			// should be something like... location.Set = 

		// INDEXER
		internal new PuzzlePiece this[int r, int c]
		{
			get
			{
				if (IsValid(r, c))
					return _board[r, c];
				else
					throw new IndexOutOfRangeException("Row and Column [r, c] coordinates in puzzle board are not identified as an Internal Medial puzzle piece.\n" +
						"Try in range: [ r[1─(width-2)], c[1─(height-2)] ]\n");
			}

			set
			{
				if (IsValid(r, c))
					_board[r, c] = value;
			}
		}


		internal Medial()
		{
			Name = LOCATION.MEDIAL;
			Position = // TODO: place in everything but corners and edges...maybe start with list to .Add() and convert to Array
		}



		// METHODS
		private bool IsValid(int r, int c)
		{
			return r != 0 && r != Length - 1 && c != 0 && c != Length - 1;
		}
	}
}
