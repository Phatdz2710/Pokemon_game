using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class GameBoard : Panel
    {
        public GameBoard()
        {
            this.AutoSize = true;
            this.Location = new Point(0, 0);
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Transparent;
        }

        
    }
}
