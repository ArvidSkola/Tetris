using System.Collections.Generic;
//Den här klassen hanterar blocken. Varje block kommer ha sin egen klass men den här klassen inefattar mycket av funktionerna som blocken kommer ha och kommer att anropas i de andra klasserna.//
namespace Tetris
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffset { get; }
        public abstract int Id { get; }
        private int RotationState;
        private Position offset;

        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }
        public IEnumerable<Position> TilePosistion()
        {
            foreach Position p in Tiles[RotationState]
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }
        public void Rotate_CW()
        {
            RotationState = (RotationState + 1) % Tiles.Lenght;
        }
        public void Rotate_CounterCW()
        {
            if(RotationState == 0)
            {
                RotationState = Tiles.Length - 1;
            }
            else 
            {
                RotationState = RotationState--;
            }
        }
        public void Move_Block(int rows,int columns)
        {
            offset.Row += rows;
            óffset.Column += columns;
        }
        public void Reset()
        {
            RotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }

    }
}
