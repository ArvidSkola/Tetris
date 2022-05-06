using System;
namespace Tetris 
    //Den här klassen kommer kombinera alla funktioner i de andra .cs klasserna för att hantera hur spelet ändras medans man spelar det.//
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
//Här skapas variabler och booler som kommer hantera poängsystemet, förlust, sparade block och om man kan spara ett block eller inte.//
        public Game_State()
        {
            Grid = new Grd(22, 10);
            Block_Queue = new Block_Queue;
            CurrentBlock = Block_Queue.Generate_Block();
             CanHold = true;
            CanHold2 = true;
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
        //Att veta om ett block får plats är viktigt för programet när spelaren ska placera blocken. Boolen kommer gå i genom alla rutor under blocket för att se om de 
        // är tomma eller inte. Är minst en full kommer boolen retuneras som falsk annars retuneras den som san.//
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
         public void Hold_Block2()
        {
            if (!CanHold2)
            {
                return;
            }

            if (Held_Block2 == null)
            {
                Held_Block2 = CurrentBlock;
                CurrentBlock = Block_Queue.Generate_Block();
            }
            else
            {
                Block tmp = CurrentBlock;
                CurrentBlock = Held_Block2;
                Held_Block2 = tmp;
            }

            CanHold2 = false;

        }
        //den här funktionen hanterar sparandet av blocken. I början av spelaet skapas en bol, CanHold som är avgörande för 
        //denna funktionen. Om den är lika med falskt kommer man att retuneras och ens block kommer inte att ändras.
        //Men om den är san kommer blocket Held_Block ändras till ditt nuvarande block och ett nytt kommer att skapas.
        
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
        //Dessa funktioner kommer att kopplas till vissa knappar på ditt tangentbord i en annan fil och kommer att styra ditt block.//
        //När dessa kallas kommer de utföras så länge blocket inte får plats. Om detta är fallet kommer den motsats rörelsen ske. 
         //Tex om du vill flytta ditt block åt höger men det får 
        //inte plats kommer det flyttas åt vänster för att motverka detta.

        public bool Lose()
        {
            return !(Grid.RowEmpty(0) && Grid.RowEmpty(1));
        }

       private void Place_Block()
        {
            foreach (Position p in CurrentBlock.TilePosistion())
            {
                Grid[p.Row, p.Column] = CurrentBlock.Id;
            }
            if(!CanHold)
            {
                CanHold = true;
            }
            if(!CanHold2)
            {
                CanHold2 = true;
            }
            Score = Score + Grid.ClearFullRows();

            if (Lose())
            {
                YouLose = true;
            }
           
            else
            {
                CurrentBlock = Block_Queue.Generate_Block();
                

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
        //Denna funktionen kommer att flytta ditt block nedåt och kommer både styras med knapptryck samt hända konstant under spelets gång.
        //Precis som i de andra funktionerna som hanterade rörelse kommer den motsatta rörelsen ske om blocket inte får plats dvs det flyttas uppåt.
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
