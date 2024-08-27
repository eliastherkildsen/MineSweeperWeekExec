using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MineSweeper
{

    public delegate void ButtonClicked(int i);

    class Tile : Button
    {

        public static event ButtonClicked buttonClicked; 
                    
        private bool isBomb {get;}
        private int boombsAround {  get; set; }

        public Tile(bool isBomb) {
            this.isBomb = isBomb;

            Content = "?";

            if (isBomb)
            {
                Content += "&"; 
            }

        }



    }
}
