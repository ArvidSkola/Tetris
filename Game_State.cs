using System;
namespace Tetris 
{
    public class Game_State
    {
        private Block currentBlock;
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);
                    if (BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }
        public Grid Grid { get; }
        public Block_Queue Block_Queue { get; }
        public bool Lose { get; private set; }
        public int Score { get; private set; }
        public Block Held_Blocks { get; private set; }
        public bool CanHold { get; private set; }

        public Game_State()
        {
            Grid = new Grd(22, 10);
            Block_Queue = new Block_Queue;
            CurrentBlock = Block_Queue.Generate_Block();
            CanHold = true;
            Held_Blocks = 0;
        }

        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.TilePosition())
            {
                if (!Grid.CellEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }
        public void Hold_Block()
        {
            if (!CanHold)
            {
                return;
            }
            if (Held_Blocks < 2)
            {
                Held_Blocks = CurrentBlock;
                CurrentBlock = Block_Queue.Generate_Block();
            }

        }
        public void Block_Rotate_CW()
        {
            CurrentBlock.Rotate_CW();
            if (!BlockFits())
            {
                CurrentBlock.Rotate_CounterCW();
            }
        }
        public void Block_Rotate_CounterCW()
        {
            CurrentBlock.Rotate_CounterCW();
            if (!BlockFits())
            {
                CurrentBlock.Rotate_CW();
            }
        }
        public void Block_Move_Left()
        {
            CurrentBlock.Move_Block(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move_Block(0, 1);
            }
        }
        public void Block_Move_Right()
        {
            CurrentBlock.Move_Block(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move_Block(0, -1);
            }
        }

        public bool Lose()
        {
            return !(Grid.RowEmpty(0) && Grid.RowEmpty(1));
        }

        private void Place_Block()
        {
            foreach (Position p in CurrentBlock.TilesPositions())
            {
                Grid[p.Row, p.Column] = CurrentBlock.Id;
            }

            Score = +Grid.ClearFullRows();

            if (Lose())
            {
                YouLose = true,
        }
            else
            {
                CurrentBlock = Block_Queue.Generate_Block;
                CanHold = true;
            }

        }

        public void Block_Move_Down()
        {
            CurrentBlock.Move_Block(1, 0);
            if (!BlockFits())
            {
                CurrentBlock.Move_Block(-1, 0);
                Place_Block();
            }
        }
        private int TileDropDistance()
        {
            int drop = 0;
            while (Grid.CellEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }
            return drop;
        }

        public int BlockDropDistance()
        {
            int drop = Grid.Rows;
            foreach (Psosition p in CurrentBlock.TilesPostition)
            {
                drop = System.Math.Min(drop, TileDropDistance(p))
            }

            return drop;
        }
        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance, 0)
            Place_Block();
        }

    }

}
