using System;
namespace Tetris
	//den här klassen hanterar blockens position.//
{
	public class Position
	{
		public int Row { get; set; }
		public int Column { get; set; }

		public Position(int row, int column)
		{
			Row  =  row;
			Column = column;
		}
	}
}
	
