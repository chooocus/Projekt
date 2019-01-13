using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class AI
    {
        //public static int MovesToGameEnd(Plansza plansza, int freeCells, ref List<Pole> freeBlockedCells)
        //{

        //    int freeBlockedCellsCount = freeBlockedCells.Count;
        //    int moves = 0;
        //    for (int i = 0; i < plansza.Rozmiar; i++)
        //        for (int j = 0; j < plansza.Rozmiar; j++)
        //        {
        //            //corner cases to be hadled later
        //            if ((i == 0 && j == 0) || (i == 0 && j == plansza.Rozmiar - 1) || (i == plansza.Rozmiar - 1 && j == 0) || (i == plansza.Rozmiar - 1 && j == plansza.Rozmiar - 1))
        //                continue;
        //            if (plansza.Pola[i, j].zajety == false && !freeBlockedCells.Exists(p => p.X == i && p.Y == j))
        //            {
        //                // border continuity xD
        //                int k = i - 1 < 0 ? plansza.Rozmiar - 1 : i - 1;
        //                int l = j - 1 < 0 ? plansza.Rozmiar - 1 : j - 1;
        //                int m = i + 1 == plansza.Rozmiar ? 0 : i + 1;
        //                int n = j + 1 == plansza.Rozmiar ? 0 : j + 1;

        //                if (plansza.Pola[i, n].zajety == true && plansza.Pola[i, l].zajety == true
        //                    && plansza.Pola[m, j].zajety == true && plansza.Pola[k, j].zajety == true)
        //                {
        //                    freeBlockedCellsCount++;
        //                    freeBlockedCells.Add(new Pole(i, j));
        //                }
        //            }
        //        }
        //    int odds = SearchForOddStructures(plansza, plansza.freeCells);
        //    moves = ((freeCells - 3 * odds - freeBlockedCellsCount) / 2) + odds;

        //    return moves;
        //}
        //public static int CalculateMaxMoves(Plansza board, List<Pole> freeCells)
        //{
        //    int size = board.Rozmiar;
        //    int moves = 0;
        //    List<Pole> visited = new List<Pole>();
        //    Pole[] friends = { new Pole(0, 1), new Pole(-1, 0), new Pole(0, -1), new Pole(1, 0) };
        //    foreach (Pole free in freeCells)
        //    {
        //        if (visited.Contains(visited.Find(v => v.X == free.X && v.Y == free.Y)))
        //            continue;
        //        for (int i = 0; i < 4; i++)
        //        {
        //            Pole temp = new Pole(Plansza.clampIndex(free.X + friends[i].X, board), Plansza.clampIndex(free.Y + friends[i].Y, board));
        //            if (!visited.Contains(visited.Find(v => v.X == temp.X && v.Y == temp.Y)) && board.Pola[temp.X, temp.Y].zajety == false)
        //            {
        //                moves++;
        //                visited.Add(free); visited.Add(temp);
        //                break;
        //            }
        //        }
        //    }
        //    return moves;
        //}
        public static int CalculateMaxMoves2(Plansza plansza, List<Pole> freeCells)
        {
            int moves = 0;
            int size = plansza.Rozmiar;
            List<Pole> visited = new List<Pole>();
            Pole[] friends = { new Pole(-1, 0), new Pole(0, -1), new Pole(1, 0), new Pole(0, 1) };
            foreach (Pole free in freeCells)
            {
                if (visited.Contains(visited.Find(v => v.X == free.X && v.Y == free.Y)))
                    continue;
                for (int i = 0; i < 4; i++)
                {
                    //border case, to be handled later
                    if (free.X + friends[i].X < 0 || free.X + friends[i].X >= size || free.Y + friends[i].Y < 0 || free.Y + friends[i].Y >= size)
                        continue;
                    Pole temp = new Pole(Plansza.clampIndex(free.X + friends[i].X, plansza), Plansza.clampIndex(free.Y + friends[i].Y, plansza));
                    if (!visited.Contains(visited.Find(v => v.X == temp.X && v.Y == temp.Y)) && plansza.Pola[temp.X, temp.Y].zajety == false)
                    {
                        moves++;
                        visited.Add(free); visited.Add(temp);
                        break;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (!(free.X + friends[i].X < 0 || free.X + friends[i].X >= size || free.Y + friends[i].Y < 0 || free.Y + friends[i].Y >= size))
                        continue;
                    Pole temp = new Pole(Plansza.clampIndex(free.X + friends[i].X, plansza), Plansza.clampIndex(free.Y + friends[i].Y, plansza));
                    if (!visited.Contains(visited.Find(v => v.X == temp.X && v.Y == temp.Y)) && plansza.Pola[temp.X, temp.Y].zajety == false)
                    {
                        moves++;
                        visited.Add(free); visited.Add(temp);
                        break;
                    }
                }
            }

            return moves;
        }


        //public static int SearchForOddStructures(Plansza plansza, List<Pole> freeUnblockedCells)
        //{
        //    int oddStructures = 0;
        //    List<Pole> visited = new List<Pole>();
        //    Pole[] friends = { new Pole(-1, 0), new Pole(1, 0), new Pole(0, -1), new Pole(0, 1) };
        //    foreach (Pole p in freeUnblockedCells)
        //    {
        //        if (visited.Contains(p))
        //            continue;
        //        int friendsCount = 0;
        //        List<Pole> freeFriends = new List<Pole>();
        //        foreach (Pole f in friends)
        //        {

        //            if (plansza.Pola[Plansza.clampIndex(p.X + f.X, plansza), Plansza.clampIndex(p.Y + f.Y, plansza)].zajety == false)
        //            {
        //                freeFriends.Add(new Pole(Plansza.clampIndex(p.X + f.X, plansza), Plansza.clampIndex(p.Y + f.Y, plansza)));
        //                friendsCount++;
        //            }
        //        }
        //        if (friendsCount == 2)
        //        {
        //            int isOddLine = 0;
        //            int isOddCurve = 0;
        //            foreach (Pole freeF in freeFriends)
        //            {
        //                int friendsOccupiedLine = 0;
        //                int friendsOccupiedCurve = 0;
        //                foreach (Pole f in friends)
        //                {
        //                    if (freeFriends[0].X == freeFriends[1].X || freeFriends[0].Y == freeFriends[1].Y)
        //                    {
        //                        if (plansza.Pola[Plansza.clampIndex(freeF.X + f.X, plansza), Plansza.clampIndex(freeF.Y + f.Y, plansza)].zajety == true)
        //                        {
        //                            friendsOccupiedLine++;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (plansza.Pola[Plansza.clampIndex(freeF.X + f.X, plansza), Plansza.clampIndex(freeF.Y + f.Y, plansza)].zajety == true)
        //                        {
        //                            friendsOccupiedCurve++;
        //                        }
        //                    }
        //                }
        //                if (friendsOccupiedLine == 3)
        //                    isOddLine++;
        //                else if (friendsOccupiedCurve == 3)
        //                    isOddCurve++;
        //            }
        //            if (isOddLine == 2)
        //            {
        //                oddStructures++;
        //                visited.Add(new Pole(freeFriends[0].X, freeFriends[0].Y));
        //                visited.Add(new Pole(freeFriends[1].X, freeFriends[1].Y));
        //            }
        //            else if (isOddCurve >= 1)
        //            {
        //                oddStructures++;
        //                visited.Add(new Pole(freeFriends[0].X, freeFriends[0].Y));
        //                visited.Add(new Pole(freeFriends[1].X, freeFriends[1].Y));
        //            }
        //        }
        //    }
        //    return oddStructures;
        //}

        public static Ruch GenerateMove(Plansza plansza, List<Pole> unblockedFreeCells)
        {
            //decide odd or even moves to win
            bool isOdd;
            bool isOddPlansza;
            int moves = CalculateMaxMoves2(plansza, plansza.freeCells);
            isOddPlansza = moves % 2 == 0 ? true : false;
            Ruch move = new Ruch(null, null);
            Pole[] nb = { new Pole(-1, 0), new Pole(1, 0), new Pole(0, -1), new Pole(0, 1) };
            foreach(Pole pole in unblockedFreeCells)
            {
                for (int i = 0; i < 3; i++)
                {
                    Pole p2 = new Pole(Plansza.clampIndex(pole.X + nb[i].X, plansza), Plansza.clampIndex(pole.Y + nb[i].Y, plansza));
                    if (plansza.Pola[p2.X, p2.Y].zajety == false)
                    {
                        move = new Ruch(pole, p2);
                        isOdd = CheckIfNumberOfBlockedFreeCellsCreatedByMoveIsOdd(plansza, move, unblockedFreeCells);
                        if (isOdd == isOddPlansza)
                            return move;

                    }
                }
                //        foreach (Pole p in unblockedFreeCells)
                //{
                //    }
                //    }
                //    //find free neighbours
                //    //for found neighbours analyze movment impact

                //}

            }
            return move;
        }
        public static bool CheckIfNumberOfBlockedFreeCellsCreatedByMoveIsOdd(Plansza plansza, Ruch move, List<Pole> freeCells)
        {
            int blockedFreeCells = 0;
            Pole[] closeFriends = { new Pole(-1, 0), new Pole(1, 0), new Pole(0, -1), new Pole(0, 1) };
            //main logic
            //for point 1
            //Check close friends
            foreach (Pole p in closeFriends)
            {
                Pole checkedFriend = new Pole(Plansza.clampIndex(move.p1.X + p.X, plansza), Plansza.clampIndex(move.p1.Y + p.Y, plansza));
                if ((checkedFriend.X != move.p2.X || checkedFriend.Y != move.p2.Y) && plansza.Pola[checkedFriend.X, checkedFriend.Y].zajety == false)
                {
                    //check close friends of that close friend
                    bool blockedCell = true;
                    foreach (Pole pp in closeFriends)
                    {
                        Pole checkedFriendOfFriend = new Pole(Plansza.clampIndex(checkedFriend.X + pp.X, plansza), Plansza.clampIndex(checkedFriend.Y + p.Y, plansza));
                        if ((checkedFriendOfFriend.X == move.p1.X && checkedFriendOfFriend.X == move.p1.Y) && plansza.Pola[checkedFriendOfFriend.X, checkedFriendOfFriend.Y].zajety == true) continue;
                        if (plansza.Pola[checkedFriendOfFriend.X, checkedFriendOfFriend.Y].zajety == false)
                        {
                            blockedCell = false;
                            break;
                        }

                    }
                    if (blockedCell) blockedFreeCells++;
                }
                checkedFriend = new Pole(Plansza.clampIndex(move.p2.X + p.X, plansza), Plansza.clampIndex(move.p2.Y + p.Y, plansza));
                if ((checkedFriend.X != move.p1.X || checkedFriend.Y != move.p1.Y) && plansza.Pola[checkedFriend.X, checkedFriend.Y].zajety == false)
                {
                    //check close friends of that close friend
                    bool blockedCell = true;
                    foreach (Pole pp in closeFriends)
                    {
                        Pole checkedFriendOfFriend = new Pole(Plansza.clampIndex(checkedFriend.X + pp.X, plansza), Plansza.clampIndex(checkedFriend.Y + p.Y, plansza));
                        if ((checkedFriendOfFriend.X == move.p2.X && checkedFriendOfFriend.X == move.p2.Y) && plansza.Pola[checkedFriendOfFriend.X, checkedFriendOfFriend.Y].zajety == true) continue;
                        if (plansza.Pola[checkedFriendOfFriend.X, checkedFriendOfFriend.Y].zajety == false)
                        {
                            blockedCell = false;
                            break;
                        }

                    }
                    if (blockedCell) blockedFreeCells++;
                }
            }
            if (blockedFreeCells == 1)
            {
                plansza.Pola[move.p1.X, move.p1.Y].zajety = true;
                plansza.Pola[move.p2.X, move.p2.Y].zajety = true;
                int l = freeCells.IndexOf(freeCells.Find(p => p.X == move.p1.X && p.Y == move.p1.Y));
                int m = freeCells.IndexOf(freeCells.Find(p => p.X == move.p2.X && p.Y == move.p2.Y));
                freeCells.RemoveAll(p => p.X == move.p1.X && p.Y == move.p1.Y);
                freeCells.RemoveAll(p => p.X == move.p2.X && p.Y == move.p2.Y);
                int moves = CalculateMaxMoves2(plansza, freeCells);
                plansza.Pola[move.p1.X, move.p1.Y].zajety = false;
                plansza.Pola[move.p2.X, move.p2.Y].zajety = false;
                freeCells.Insert(l, move.p1);
                freeCells.Insert(m, move.p2);
                if (moves % 2 == 0)
                    return true;
                else
                    return false;
            }
            return (blockedFreeCells >=2 ? true : false);
        }
        public static bool CheckIfNumberOfMovesCreatedByMoveIsOdd(Plansza plansza, Ruch move)
        {
            Plansza.OccupyCells(move, plansza);
            int moves = CalculateMaxMoves2(plansza, plansza.freeCells);
            bool isOdd = moves % 2 == 0 ? true : false;
            Plansza.DeoccupyCells(move, plansza);
            return isOdd;
        }
    }
}
