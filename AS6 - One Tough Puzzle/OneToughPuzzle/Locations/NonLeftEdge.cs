using System;
using System.Collections.Generic;
using System.Text;

namespace OneToughPuzzle.Locations
{
	internal class NonLeftEdge : Puzzle
	{
		// PROPERTIES
		internal override LOCATION Location { get; }

		// CONSTRUCTOR
		internal NonLeftEdge()
		{
			Location = LOCATION.NON_LEFTEDGE;
		}

		// METHODS
		internal override void SetPiece(int i, int r, int c)
		{
			if (c > 0 && c < Length && r >= 0 && r < Length)
			{
				Pieces[i].InRow = r;
				Pieces[i].InColumn = c;
				Pieces[i].IsA = LOCATION.NON_LEFTEDGE;
			}

		}
	}
}
