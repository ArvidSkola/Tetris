using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//här hanteras O blockets olika rotationer.//
namespace Tetris
{
    public class O_Block:Block 
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[]{new(0,0),new(0,1),new(1,0),new(1,1)}
        };
        //Eftersom O blockets position inte ändras om man roterar det behöver man inte ha mer än en position i arrayen.//
        public override int Id => 2;
        protected override Position StartOffset => new Position(0, 4);
        protected override Position[][] Tiles => tiles;
    }
}
