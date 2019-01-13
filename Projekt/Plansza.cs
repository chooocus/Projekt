using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class Plansza
    {
        public bool isStarted = false;
        public int Rozmiar { get; set; }
        public Pole[,] Pola { get; set; }
        public List<Pole> occupiedCells = new List<Pole>();
        public List<Pole> freeCells = new List<Pole>();
        public List<Pole> freeBlockedCells = new List<Pole>();
        public List<Pole> freeUnblockedCells = new List<Pole>();
        public static void AddFreeCellsToList(Plansza plansza)
        {
            for (int i = 0; i < plansza.Rozmiar; i++)
                for (int j = 0; j < plansza.Rozmiar; j++)
                    if (plansza.Pola[i, j].zajety == false)
                        plansza.freeCells.Add(new Pole(i, j));
        }
        public static void AddFreeUnblockedCellsToList(Plansza plansza)
        {
            foreach (Pole pole in plansza.freeCells)
            {
                Pole[] nb = { new Pole(-1, 0), new Pole(1, 0), new Pole(0, -1), new Pole(0, 1) };
                bool added = false;
                foreach (Pole n in nb)
                {

                    Pole p2 = new Pole(Plansza.clampIndex(pole.X + n.X, plansza), Plansza.clampIndex(pole.Y + n.Y, plansza));
                    if (plansza.Pola[p2.X, p2.Y].zajety == false)
                    {
                        added = true;
                    }
                }
                if(added)
                    plansza.freeUnblockedCells.Add(new Pole(pole.X, pole.Y));
            }
        }
        public static void OccupyCells(Plansza plansza, string r)
        {
            for (int m = 0; m < r.Length; m++)
            {
                switch (r[m])
                {
                    case '{':
                        int n = m + 1;
                        string x1 = "";
                        string y1 = "";
                        while (!r[n].Equals(';'))
                        {
                            x1 += r[n];
                            n++;
                        }
                        n++;
                        while (!r[n].Equals('}'))
                        {
                            y1 += r[n];
                            n++;
                        }
                        m = n;
                        int x = Int32.Parse(x1);
                        int y = Int32.Parse(y1);

                        Pole poleDoWyrzucenia = new Pole(x, y);

                        plansza.occupiedCells.Add(poleDoWyrzucenia);
                        plansza.freeCells.RemoveAll(p => p.X == x && p.Y == y);

                        plansza.Pola[x, y].zajety = true;
                if (n == r.Length - 1)
                {
                    plansza.freeUnblockedCells.Clear();
                    AddFreeUnblockedCellsToList(plansza);
                }
                        break;
                    default:
                        break;
                }
            }
        }
        public static void OccupyCells(Ruch move, Plansza plansza)
        {
            plansza.Pola[move.p1.X, move.p1.Y].zajety = true;
            plansza.Pola[move.p2.X, move.p2.Y].zajety = true;
            plansza.occupiedCells.Add(move.p1);
            plansza.occupiedCells.Add(move.p2);
            plansza.freeCells.RemoveAll(p => p.X == move.p1.X && p.Y == move.p1.Y);
            plansza.freeCells.RemoveAll(p => p.X == move.p2.X && p.Y == move.p2.Y);
            plansza.freeUnblockedCells.Clear();
            AddFreeUnblockedCellsToList(plansza);
        }
        public static void DeoccupyCells(Ruch move, Plansza plansza)
        {
            plansza.Pola[move.p1.X, move.p1.Y].zajety = false;
            plansza.Pola[move.p2.X, move.p2.Y].zajety = false;
            plansza.occupiedCells.RemoveAll(p => p.X == move.p1.X && p.Y == move.p1.Y); 
            plansza.occupiedCells.RemoveAll(p => p.X == move.p2.X && p.Y == move.p2.Y); 
            plansza.freeCells.Add(move.p1);
            plansza.freeCells.Add(move.p2);
            plansza.freeUnblockedCells.Clear();
            AddFreeUnblockedCellsToList(plansza);

        }
        public static int clampIndex(int index, Plansza plansza)
        {
            if (index < 0)
                return plansza.Rozmiar + index;
            else if (index >= plansza.Rozmiar)
                return index % plansza.Rozmiar;
            return index;
        }
        public static Ruch nextMove(Plansza plansza)
        {
            // if (freeCells.Count > 30)
            //    return randomMove();
            Ruch move;
            move = AI.GenerateMove(plansza, plansza.freeUnblockedCells);
            OccupyCells(move, plansza);
            return move;
        }
    }

}
