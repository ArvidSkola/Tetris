using System;

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
    }

    private bool BlockFits()
    {
        foreach (position p in CurrentBlock.TilePosition())
        {
            if(!Grid.CellEmpty(p.Row, p.Column))
            {
                return false;
            }
        }
        return true;
    }
}
