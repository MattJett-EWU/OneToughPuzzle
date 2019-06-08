using System;
using System.Diagnostics;
using System.Linq;

namespace OneToughPuzzle
{
	public class PuzzlePiece
	{
		#region ENUMS
		internal enum CONNECT_TO
		{
			ABOVE,
			RIGHT,
			BELOW,
			LEFT
		}
		private enum LOCATIONS
		{
			TOP,
			RIGHT,
			BOTTOM,
			LEFT
		}
		internal enum TAB
		{
			CIRCLE_PROTRUDE = 1,
			SQUARE_PROTRUDE = 2,
			TRIANGLE_PROTRUDE = 3,
			DOUBLETRIANGLE_PROTRUDE = 4,

			CIRCLE_INTRUDE = -1,
			SQUARE_INTRUDE = -2,
			TRIANGEL_INTRUDE = -3,
			DOUBLETRIANGLE_INTRUDE = -4
		}
		#endregion

		#region FIELDS
		internal static readonly PuzzlePiece PIECE_0 = new PuzzlePiece("Piece #0", new TAB[] {TAB.CIRCLE_PROTRUDE, TAB.DOUBLETRIANGLE_PROTRUDE, TAB.SQUARE_INTRUDE, TAB.CIRCLE_INTRUDE}, "#0");
		internal static readonly PuzzlePiece PIECE_1 = new PuzzlePiece("Piece #1", new TAB[] {TAB.CIRCLE_PROTRUDE, TAB.CIRCLE_PROTRUDE, TAB.DOUBLETRIANGLE_INTRUDE, TAB.DOUBLETRIANGLE_INTRUDE}, "#1");
		internal static readonly PuzzlePiece PIECE_2 = new PuzzlePiece("Piece #2", new TAB[] {TAB.SQUARE_PROTRUDE, TAB.TRIANGLE_PROTRUDE, TAB.SQUARE_INTRUDE, TAB.DOUBLETRIANGLE_INTRUDE}, "#2");
		internal static readonly PuzzlePiece PIECE_3 = new PuzzlePiece("Piece #3", new TAB[] {TAB.DOUBLETRIANGLE_PROTRUDE, TAB.CIRCLE_PROTRUDE, TAB.CIRCLE_INTRUDE, TAB.DOUBLETRIANGLE_INTRUDE}, "#3");
		internal static readonly PuzzlePiece PIECE_4 = new PuzzlePiece("Piece #4", new TAB[] {TAB.DOUBLETRIANGLE_PROTRUDE, TAB.DOUBLETRIANGLE_PROTRUDE, TAB.SQUARE_INTRUDE, TAB.CIRCLE_INTRUDE}, "#4");
		internal static readonly PuzzlePiece PIECE_5 = new PuzzlePiece("Piece #5", new TAB[] {TAB.SQUARE_PROTRUDE, TAB.TRIANGLE_PROTRUDE, TAB.DOUBLETRIANGLE_INTRUDE, TAB.DOUBLETRIANGLE_INTRUDE}, "#5");
		internal static readonly PuzzlePiece PIECE_6 = new PuzzlePiece("Piece #6", new TAB[] {TAB.CIRCLE_PROTRUDE, TAB.SQUARE_PROTRUDE, TAB.TRIANGEL_INTRUDE, TAB.DOUBLETRIANGLE_INTRUDE}, "#6");
		internal static readonly PuzzlePiece PIECE_7 = new PuzzlePiece("Piece #7", new TAB[] {TAB.CIRCLE_PROTRUDE, TAB.DOUBLETRIANGLE_PROTRUDE, TAB.DOUBLETRIANGLE_INTRUDE, TAB.CIRCLE_INTRUDE}, "#7");
		internal static readonly PuzzlePiece PIECE_8 = new PuzzlePiece("Piece #8", new TAB[] {TAB.DOUBLETRIANGLE_PROTRUDE, TAB.CIRCLE_PROTRUDE, TAB.SQUARE_INTRUDE, TAB.CIRCLE_INTRUDE}, "#8");
		#endregion

		#region PROPERTIES
		string Name { get; }
		string ShortName { get; }
		TAB[] ContainingTabs { get; set; } 
		#endregion

		#region INDEXER
		internal TAB this[int i] => ContainingTabs[i];
		#endregion

		#region CONSTRUCTOR
		PuzzlePiece(string pieceName, TAB[] tabs, string shortName)
		{
			Name = pieceName;
			ContainingTabs = tabs;
			ShortName = shortName;
		}
		#endregion


		#region METHODS
		/// <summary>
		///		Each call to this method will rotate this puzzle piece clockwise by 90 degrees.
		///		Found help on how to do this online by user "Robert Fricke".
		/// </summary>
		/// <see cref="https://stackoverflow.com/questions/21385066/shifting-array-elements-to-right"/>
		internal PuzzlePiece Rotate()
		{
			var newArrayOfRotatedTabs = new TAB[4];
			for (int i = 0; i < ContainingTabs.Length; i++)
				newArrayOfRotatedTabs[(i + 1) % ContainingTabs.Length] = ContainingTabs[i];
			ContainingTabs = newArrayOfRotatedTabs;
			return this;
		}

		/// <summary>
		///		Tests connection of this puzzle piece against surrounding pieces.
		/// </summary>
		/// <param name="board">The puzzle board itself.</param>
		/// <param name="r">Row of this piece.</param>
		/// <param name="c">Column of this piece.</param>
		/// <returns>True: Piece fits in puzzle. Automatically true if top-left corner piece placement. False: Piece doesn't fit in puzzle.</returns>
		internal bool Connects(PuzzlePiece[,] board, int r, int c)
		{
			if (r == 0 && c > 0)
				return (int)this[(int)CONNECT_TO.LEFT] + board[r, c - 1][(int)CONNECT_TO.RIGHT] == 0;
			if (r > 0 && c == 0)
				return (int)this[(int)CONNECT_TO.ABOVE] + board[r - 1, c][(int)CONNECT_TO.BELOW] == 0;
			if (r > 0 && c > 0)
				return ((int)this[(int)CONNECT_TO.LEFT] + board[r, c - 1][(int)CONNECT_TO.RIGHT] == 0) &&
					   ((int)this[(int)CONNECT_TO.ABOVE] + board[r - 1, c][(int)CONNECT_TO.BELOW] == 0);
			else
				return true;
		}

		public override string ToString()
		{
			var flavorNames = new System.Collections.Generic.List<string>();
			string tabs = "";
			Array.ForEach(ContainingTabs, tab => flavorNames.Add(Enum.GetName(typeof(LOCATIONS), Array.IndexOf(ContainingTabs, tab)) + ": " + tab.ToString()));
			string last = flavorNames.Last();

			flavorNames.ForEach(e =>
			{
				tabs += e;
				if (!e.Equals(last))
					tabs += ", ";
			});

			return string.Format("{0}\t{1}", Name, "{" + tabs + "}");
		}

		public string PieceToString()
		{
			// TODO: Maybe include this all in Puzzle class, because it's counting each occurence on a new line
				// need to format string to build all calls for three seperate pieces on board by the row.
				// so in Puzzle class, have one For() loop looping to Length, each call with print 3 pieces side by side.
			string[] tabs = new string[4];
			for (int i = 0; i < tabs.Length; i++)
			{
				switch (ContainingTabs[i])
				{
					case TAB.CIRCLE_PROTRUDE:
						tabs[i] = "C+";
						break;
					case TAB.CIRCLE_INTRUDE:
						tabs[i] = "C-";
						break;
					case TAB.DOUBLETRIANGLE_PROTRUDE:
						tabs[i] = "D+";
						break;
					case TAB.DOUBLETRIANGLE_INTRUDE:
						tabs[i] = "D-";
						break;
					case TAB.SQUARE_PROTRUDE:
						tabs[i] = "S+";
						break;
					case TAB.SQUARE_INTRUDE:
						tabs[i] = "S-";
						break;
					case TAB.TRIANGLE_PROTRUDE:
						tabs[i] = "T+";
						break;
					case TAB.TRIANGEL_INTRUDE:
						tabs[i] = "T-";
						break;
				}
			}
			return string.Format("  ____{0}____" +
							  "\n |          |" +
							  "\n |          |" +
							  "\n {3}   {4}   {1}" +
							  "\n |          |" +
							  "\n |____{2}____|\n", tabs[0], tabs[1], tabs[2], tabs[3], ShortName);
		}

		public static string PieceToString(params PuzzlePiece[] pieces)
		{
			// TODO: Maybe include this all in Puzzle class, because it's counting each occurence on a new line
			// need to format string to build all calls for three seperate pieces on board by the row.
			// so in Puzzle class, have one For() loop looping to Length, each call with print 3 pieces side by side.
			string[] pieceLeftSide = new string[4];
			string[] pieceMiddle = new string[4];
			string[] pieceRightSide = new string[4];

			pieceLeftSide = Label(pieceLeftSide, pieces[0]);
			pieceMiddle = Label(pieceMiddle, pieces[1]);
			pieceRightSide = Label(pieceRightSide, pieces[2]);

			return string.Format("  ____{0}____      ____{5}____      ____{10}____" +
							  "\n |          |	 |          |    |          |" +
							  "\n |          |	 |          |    |          |" +
							  "\n {3}   {4}    {1}   {8}   {9}    {6}   {13}   {14}    {11}" +
							  "\n |          |	 |          |    |          |" +
							  "\n |____{2}____|	 |____{7}____|    |____{12}____|"
							  , pieceLeftSide[0], pieceLeftSide[1], pieceLeftSide[2], pieceLeftSide[3], pieces[0].ShortName,
							    pieceMiddle[0], pieceMiddle[1], pieceMiddle[2], pieceMiddle[3], pieces[1].ShortName,
								pieceRightSide[0], pieceRightSide[1], pieceRightSide[2], pieceRightSide[3], pieces[2].ShortName);
		}

		static string[] Label(string[] labels, PuzzlePiece piece)
		{
			for (int i = 0; i < labels.Length; i++)
			{
				switch (piece.ContainingTabs[i])
				{
					case TAB.CIRCLE_PROTRUDE:
						labels[i] = "C+";
						break;
					case TAB.CIRCLE_INTRUDE:
						labels[i] = "C-";
						break;
					case TAB.DOUBLETRIANGLE_PROTRUDE:
						labels[i] = "D+";
						break;
					case TAB.DOUBLETRIANGLE_INTRUDE:
						labels[i] = "D-";
						break;
					case TAB.SQUARE_PROTRUDE:
						labels[i] = "S+";
						break;
					case TAB.SQUARE_INTRUDE:
						labels[i] = "S-";
						break;
					case TAB.TRIANGLE_PROTRUDE:
						labels[i] = "T+";
						break;
					case TAB.TRIANGEL_INTRUDE:
						labels[i] = "T-";
						break;
				}
			}
			return labels;
		}
		#endregion
	}
}
