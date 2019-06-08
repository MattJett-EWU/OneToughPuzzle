using System;
using System.Collections.Generic;
using System.Text;

namespace OneToughPuzzle
{
	internal class TopLeft_Corner : PuzzleLocation
	{
		// FIELDS

		// PROPERTIES
		internal LOCATION Name { get; }
		internal override LOCATION Position { get; }
		internal PuzzlePiece Set { get => _board[0, 0]; set => TopLeft_Corner = value; }

		// INDEXER


		internal TopLeft_Corner()
		{
			Name = LOCATION.TOPLEFT_CORNER;
			Position = 
		}



		// METHODS
		private bool IsValid(int r, int c)
		{
			return r != 0 && r != Length - 1 && c != 0 && c != Length - 1;
		}
	}
}
