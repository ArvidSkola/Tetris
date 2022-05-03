using System;
//Här hanteras genereringen av blocken.//
namespace Tetris
{
    public class Block_Queue
    {
        private readonly Block[] blocks = new Block[]
        {
            new I_Block(),
            new O_Block(),
            new J_Block(),
            new L_Block(),
            new T_Block(),
            new Z_Block(),
            new S_Block()

        };
        //Arrayen innehåller alla block, genom att använda Random funktionen kommer ett av blocken slumpas fram.//
        private readonly Random random = new Random();
        public Block Next_Block { get; private set; }
        //Eftersom programet ska vissa inkomande block måste vi skapa ett sådant.//
        public Block_Queue()
        {
            Next_Block = RandomBlock();
        }
       
        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }
        public Block Generate_Block()
        {
            Block block = Next_Block;
            do
            {
                Next_Block = RandomBlock();
            }
            while (block.Id == Next_Block.Id);
            return block;
        }
        //Här genereras blocken genom att skapa ett Block,block, som sen returneras. I do while loopen sätts Next_Block som random block. 
        //Loopen kommer att avbrytas när block id et inte längre är lika med Next_Blocks id. Då retuneras blocket.//

    }

}
