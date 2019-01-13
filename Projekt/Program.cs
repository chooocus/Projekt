using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(16384)));
            Plansza plansza = new Plansza();
            //Plansza planszaChwilowa = new Plansza();
            int s = Int32.Parse(Console.In.ReadLine());
            plansza.Rozmiar = s;
            //planszaChwilowa.Rozmiar = s;
            plansza.Pola = new Pole[s,s];
            //planszaChwilowa.Pola = new Pole[s, s];
            //for (int i = 0; i < plansza.Rozmiar; i++)
            //    for (int j = 0; j < plansza.Rozmiar; j++)
            //    {
            //        planszaChwilowa.Pola[i, j] = new Pole(i, j);
            //    }
            for (int i = 0; i < s; i++)
                for (int j = 0; j < s; j++)
                {
                    plansza.Pola[i, j] = new Pole(i, j);
                }
            Plansza.AddFreeCellsToList(plansza);
            //Plansza.AddFreeCellsToList(planszaChwilowa);
            Plansza.AddFreeUnblockedCellsToList(plansza);
            //Plansza.AddFreeUnblockedCellsToList(planszaChwilowa);
            Console.WriteLine("ok");
            string o = Console.In.ReadLine();
            Plansza.OccupyCells(plansza, o);
            //Plansza.OccupyCells(planszaChwilowa, o);
            //DateTime startTime = DateTime.Now;
            //int x = AI.MovesToGameEnd(plansza, plansza.freeCells.Count(), ref plansza.freeBlockedCells);
            //DateTime stopTime = DateTime.Now;
            //TimeSpan roznica = stopTime - startTime;
            //Console.WriteLine(roznica.ToString(@"ss\.ffff") + " " + x);
            Console.WriteLine("ok");
            Programik(Console.In.ReadLine(), plansza);
        }
        public static void Programik(string message, Plansza plansza)
        {
            Ruch ruch = null;
            if (message[0].Equals('{'))
            {
                Plansza.OccupyCells(plansza, message);
                //Plansza.OccupyCells(planszaChwilowa, message);
                ruch = Plansza.nextMove(plansza);
            }
            else if (message.ToLower().Equals("start"))
            {
                plansza.isStarted = true;
                ruch = Plansza.nextMove(plansza);
                //randomMove(plansza);
            }
            else if (message.ToLower().Equals("end"))
            {
                return;
            }
            else
            {
                ruch = Plansza.nextMove(plansza);
            }
            Console.WriteLine("{" + ruch.p1.X + ";" + ruch.p1.Y + "},{" + ruch.p2.X + ";" + ruch.p2.Y + "}");
            Programik(Console.In.ReadLine(), plansza);
        }
    //    public static Ruch randomMove(Plansza plansza)
    //    {
    //        Random rnd = new Random();
    //        Ruch ruch = null;
    //        int x = rnd.Next(0, plansza.Rozmiar);
    //        int y = rnd.Next(0, plansza.Rozmiar);
    //        while (plansza.Pola[x, y].zajety == true)
    //        {
    //            x = rnd.Next(0, plansza.Rozmiar);
    //            y = rnd.Next(0, plansza.Rozmiar);
    //        }
    //        while (plansza.Pola[x, y].zajety == false)
    //        {
    //            while (plansza.Pola[x, y].zajety == true)
    //            {
    //                x = rnd.Next(0, plansza.Rozmiar);
    //                y = rnd.Next(0, plansza.Rozmiar);
    //            }
    //            for (int i = -1; i < 2; i++)
    //            {
    //                for (int j = -1; j < 2; j++)
    //                {
    //                    if ((i == -1 && (j == 1 || j == -1)) || (i == 1 && (j == 1 || j == -1)) || (i == 0 && j == 0))
    //                        break;
    //                    else if (x == 0)
    //                    {
    //                        if (y == 0)
    //                        {
    //                            if (i == -1)
    //                            {
    //                                if (!plansza.Pola[plansza.Rozmiar - 1, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(plansza.Rozmiar - 1, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (plansza.Rozmiar - 1) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                            else if (j == -1)
    //                            {
    //                                if (!plansza.Pola[x + i, plansza.Rozmiar - 1].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, plansza.Rozmiar - 1));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (plansza.Rozmiar - 1) + "}");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (!plansza.Pola[x + i, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                        }
    //                        else if (y == plansza.Rozmiar - 1)
    //                        {
    //                            if (i == -1)
    //                            {
    //                                if (!plansza.Pola[plansza.Rozmiar - 1, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(plansza.Rozmiar - 1, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (plansza.Rozmiar - 1) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                            else if (j == 1)
    //                            {
    //                                if (!plansza.Pola[x + i, 0].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, 0));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (0) + "}");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (!plansza.Pola[x + i, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (i == -1)
    //                            {
    //                                if (!plansza.Pola[plansza.Rozmiar - 1, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(plansza.Rozmiar - 1, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (plansza.Rozmiar - 1) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (!plansza.Pola[x + i, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                        }
    //                    }
    //                    else if (x == plansza.Rozmiar - 1)
    //                    {
    //                        if (y == 0)
    //                        {
    //                            if (i == 1)
    //                            {
    //                                if (!plansza.Pola[0, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(0, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (0) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                            else if (j == -1)
    //                            {
    //                                if (!plansza.Pola[x + i, plansza.Rozmiar - 1].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, plansza.Rozmiar - 1));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (plansza.Rozmiar - 1) + "}");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (!plansza.Pola[x + i, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                        }
    //                        else if (y == plansza.Rozmiar - 1)
    //                        {
    //                            if (i == 1)
    //                            {
    //                                if (!plansza.Pola[0, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(0, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (0) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                            else if (j == 1)
    //                            {
    //                                if (!plansza.Pola[x + i, 0].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, 0));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (0) + "}");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (!plansza.Pola[x + i, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        if (y == 0)
    //                        {
    //                            if (j == -1)
    //                            {
    //                                if (!plansza.Pola[x + i, plansza.Rozmiar - 1].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, plansza.Rozmiar - 1));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (plansza.Rozmiar - 1) + "}");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (!plansza.Pola[x + i, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                        }
    //                        else if (y == plansza.Rozmiar - 1)
    //                        {
    //                            if (j == 1)
    //                            {
    //                                if (!plansza.Pola[x + i, 0].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, 0));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (0) + "}");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (!plansza.Pola[x + i, y + j].zajety)
    //                                {
    //                                    ruch = new Ruch(new Pole(x, y), new Pole(x + i, y + j));
    //                                    return ruch;
    //                                    Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (y + j) + "}");
    //                                }
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (!plansza.Pola[x + i, y + j].zajety)
    //                            {
    //                                ruch = new Ruch(new Pole(x, y), new Pole(x + i, y + j));
    //                                return ruch;
    //                                Console.WriteLine("{" + x + ";" + y + "},{" + (x + i) + ";" + (y + j) + "}");
    //                            }
    //                        }
    //                    }
    //                    if (plansza.Pola[x, y].zajety)
    //                        break;
    //                }
    //                if (plansza.Pola[x, y].zajety)
    //                    break;
    //            }
    //        }
    //        return null;
    //    }
    }
}
