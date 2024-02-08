using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model.Core;

namespace Model.Controllers
{
    public static class CellController
    {
        private const int Inf = int.MaxValue;

        public static void RotateCellOf90Corner(Cell cell)
        {
            var resultMatrix = new List<int>(cell.CellMatrix.Count);
            var tempMatrix = new[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            for (var i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < tempMatrix.GetLength(0); j++)
                {
                    tempMatrix[i, j] = cell.CellMatrix[i * 3 + j];
                }
            }

            for (var i = 0; i < tempMatrix.GetLength(0); ++i)
            {
                for (var j = 0; j < tempMatrix.GetLength(0); ++j)
                {
                    resultMatrix.Add(tempMatrix[j, tempMatrix.GetLength(0) - i - 1]);
                }
            }

            for (var i = 0; i < resultMatrix.Count; i++)
            {
                cell.CellMatrix[i] = resultMatrix[i];
            }
        }

        public static bool CheckForFinish(List<Cell> cells)
        {
            var initCell = cells.First(x => x.SpecialCellType == SpecialCellType.InitCell);
            var visited = new Dictionary<int, bool>(cells.Capacity);
            var q = new Queue<int>();

            visited[initCell.Id] = true;
            q.Enqueue(initCell.Id);
            while (q.Count != 0)
            {
                var v = cells.FirstOrDefault(x => x.Id == q.Peek());
                q.Dequeue();
                if (v!.SpecialCellType == SpecialCellType.EndCell) return true;
                if (v!.upCell != null)
                {
                    if (!visited.ContainsKey(v!.upCell.Id))
                    {
                        visited.Add(v!.upCell.Id, true);
                        q.Enqueue(v!.upCell.Id);
                    }
                    else if (!visited[v!.upCell.Id])
                    {
                        visited.Add(v!.upCell.Id, true);
                        q.Enqueue(v!.upCell.Id);
                    }
                }

                if (v!.downCell != null)
                {
                    if (!visited.ContainsKey(v!.downCell.Id))
                    {
                        visited.Add(v!.downCell.Id, true);
                        q.Enqueue(v!.downCell.Id);
                    }
                    else if (!visited[v!.downCell.Id])
                    {
                        visited.Add(v!.downCell.Id, true);
                        q.Enqueue(v!.downCell.Id);
                    }
                }

                if (v!.rightCell != null)
                {
                    if (!visited.ContainsKey(v!.rightCell.Id))
                    {
                        visited.Add(v!.rightCell.Id, true);
                        q.Enqueue(v!.rightCell.Id);
                    }
                    else if (!visited[v!.rightCell.Id])
                    {
                        visited.Add(v!.rightCell.Id, true);
                        q.Enqueue(v!.rightCell.Id);
                    }
                }

                if (v!.leftCell != null)
                {
                    if (!visited.ContainsKey(v!.leftCell.Id))
                    {
                        visited.Add(v!.leftCell.Id, true);
                        q.Enqueue(v!.leftCell.Id);
                    }
                    else if (!visited[v!.leftCell.Id])
                    {
                        visited.Add(v!.leftCell.Id, true);
                        q.Enqueue(v!.leftCell.Id);
                    }
                }
            }

            return false;
        }

        public static void SetCellFriend(List<Cell> cells, Cell sender)
        {
            if (sender.upCell != null)
            {
                sender!.upCell.downCell = null;
                sender!.upCell = null;
            }

            if (sender.downCell != null)
            {
                sender!.downCell.upCell = null;
                sender!.downCell = null;
            }

            if (sender.leftCell != null)
            {
                sender!.leftCell.rightCell = null;
                sender!.leftCell = null;
            }

            if (sender.rightCell != null)
            {
                sender!.rightCell.leftCell = null;
                sender!.rightCell = null;
            }

            if (sender.CellMatrix[1] == 1) // Up
            {
                var upCell =
                    cells.FirstOrDefault(x => x.PositionY == sender.PositionY + 1 && x.PositionX == sender.PositionX);
                if (upCell is not null && upCell.CellType != CellType.None && upCell.CellMatrix[7] == 1)
                {
                    sender.upCell = upCell;
                    upCell.downCell = sender;
                }
            }

            if (sender.CellMatrix[3] == 1) //Left
            {
                var leftCell = cells.FirstOrDefault(x =>
                    x.PositionX == sender.PositionX - 1 && x.PositionY == sender.PositionY);
                if (leftCell is not null && leftCell.CellType != CellType.None && leftCell.CellMatrix[5] == 1)
                {
                    sender.leftCell = leftCell;
                    leftCell.rightCell = sender;
                }
            }

            if (sender.CellMatrix[7] == 1) //Down
            {
                var downCell =
                    cells.FirstOrDefault(x => x.PositionY == sender.PositionY - 1 && x.PositionX == sender.PositionX);
                if (downCell is not null && downCell.CellType != CellType.None && downCell.CellMatrix[1] == 1)
                {
                    sender.downCell = downCell;
                    downCell.upCell = sender;
                }
            }

            if (sender.CellMatrix[5] == 1) //Right
            {
                var rightCell =
                    cells.FirstOrDefault(x => x.PositionX == sender.PositionX + 1 && x.PositionY == sender.PositionY);
                if (rightCell is not null && rightCell.CellType != CellType.None && rightCell.CellMatrix[3] == 1)
                {
                    sender.rightCell = rightCell;
                    rightCell.leftCell = sender;
                }
            }
        }
    }
}