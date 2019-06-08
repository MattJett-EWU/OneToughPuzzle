using System;
using System.Collections.Generic;
using System.Linq;

namespace OneToughPuzzle
{
	public class Puzzle
	{
		#region CLASS UTILITIES
		// FIELDS
		const string _SEED = "#f93e532f";
		bool _solved;
		PuzzlePiece[] _collection = { PuzzlePiece.PIECE_0, PuzzlePiece.PIECE_1, PuzzlePiece.PIECE_2,
									  PuzzlePiece.PIECE_3, PuzzlePiece.PIECE_4, PuzzlePiece.PIECE_5,
									  PuzzlePiece.PIECE_6, PuzzlePiece.PIECE_7, PuzzlePiece.PIECE_8 };

		// PROPERTIES
		int Length { get; }
		internal int Steps { get; private set; }
		internal long Time { get; private set; } 
		internal string GetSeed { get; } = _SEED;
		internal PuzzlePiece[,] PuzzleBoard { get; private set; }

		// INDEXERS
		internal PuzzlePiece this[int r, int c]
		{
			get => PuzzleBoard[r, c];
			set => this[r, c] = value;
		}

		// CONSTRUCTORS
		internal Puzzle()
		{
			double w;
			if ((w = Math.Sqrt(_collection.Length)) % 1 == 0)
				PuzzleBoard = new PuzzlePiece[(int)w, (int)w];
			Length = (int)PuzzleBoard.GetLongLength(0);
		} 
		#endregion


		#region METHODS
		internal bool Place()
		{
			return Place(_collection, 0, 0);

			bool Place(PuzzlePiece[] remainingPieces, int r, int c)
			{
				var queuedPieces = new Queue<PuzzlePiece>(remainingPieces);
				_solved = queuedPieces.Count > 0 ? false : true;
				var discardedPieces = new List<PuzzlePiece>();

				while (queuedPieces.TryDequeue(out PuzzlePiece poppedPiece))
				{
					int rotation = 0;
					while (!_solved && rotation < 4)
					{
						if (poppedPiece.Connects(PuzzleBoard, r, c))
						{
							PuzzleBoard[r, c] = poppedPiece;
							int rowCopy = r, colCopy = c;
							_solved = Place(CombinePiles(queuedPieces, discardedPieces), (colCopy + 1 >= Length) ? ++rowCopy : rowCopy, (colCopy + 1 >= Length) ? colCopy = 0 : ++colCopy);
						}
						if (!_solved)
						{
							poppedPiece.Rotate();
							rotation++;
						}
						++Steps;
					}
					if (!_solved)
					{
						discardedPieces.Add(poppedPiece);
						PuzzleBoard[r, c] = null;
					}
				}
				return _solved;
			}
		}

		internal string PrintPuzzle()
		{
			string fullPuzzle = "";
			for (int r = 0; r < Length; r++)
			{
				fullPuzzle += PuzzlePiece.PieceToString(Array<PuzzlePiece>.GetRow(PuzzleBoard, r));
				fullPuzzle += "\n\n\n";
			}
			return fullPuzzle;
		}

		public override string ToString()
		{
			string s = "";
			Array.ForEach<PuzzlePiece>(_collection, piece => s += piece.ToString() + "\n");
			return s;
		} 
		#endregion

		#region PRIVATE METHODS
		PuzzlePiece[] CombinePiles(Queue<PuzzlePiece> queuePile, List<PuzzlePiece> discardPile)
		{
			var alternateQueuePile = new Queue<PuzzlePiece>(queuePile);
			discardPile.ForEach(e => alternateQueuePile.Enqueue(e));
			return alternateQueuePile.ToArray();
		} 
		#endregion
	}

	internal static class Array<T>
	{
		public static T[] GetRow(T[,] matrix, int lockRow)
		{
			return Enumerable.Range(0, matrix.GetLength(1))
				.Select(c => matrix[lockRow, c])
				.ToArray();
		}
	}
}
