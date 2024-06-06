using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Kostky_Roupa
{
    

    public class kostky
    {
        private static Random random = new Random();
        public int hodnota { get; private set; }

        public kostky()
        {
            Hod();
        }

        public void Hod()
        {
            hodnota = random.Next(1, 7);
        }
    }
}
