using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notendurchschnitt
{
    class TsNote
    {
        private double note;
        private double gewicht;

        public double Note 
        {
            get => note;
            set
            {
                if (value < 7 && value > 0)
                {
                    note = value;
                }
                else
                {
                    throw new Exception("Note muss zwischen 1 und 6 liegen.");
                }
            }
        }
        public double Gewicht { get => gewicht; }

        public TsNote(double eineNote, double einGewicht)
        {
            Note = eineNote;
            gewicht = einGewicht;
        }

        public override string ToString() => $"{Note}\t{Gewicht}";

    }
}
