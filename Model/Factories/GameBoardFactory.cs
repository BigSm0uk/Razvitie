using System;
using System.Collections.Generic;
using Model.Core;
using Model.LevelsData;
using UnityEngine;
using ViewModel.Core;

namespace Model.Factories
{
    public class GameBoardFactory : MonoBehaviour
    {
        private static readonly Dictionary<char, string> MapMapper = new()
        {
            { 'N', "None" },
            { 'L', "LineCell" },
            { 'P', "PlusCell" },
            { 'C', "CurveCell" },
            { 'I', "InitCell" },
            { 'E', "EndCell" },
        };

        public static List<Cell> Create(Cell prefab, Vector2 size, float spacing, Transform root, int gameLevel)
        {
            List<Cell> cells = new();
            LevelsData.LevelsData.Init(gameLevel);
            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y < size.y; y++)
                {
                    var position = new Vector3(x * spacing, y * spacing);
                    var (cellType, specialCellType) = InitPipeType(LevelsData.LevelsData.Map, y, x);
                    var cell = Instantiate(prefab, position + root.position, Quaternion.identity, root);
                    cell.Initialize(new Vector2Int(x, y), cellType, specialCellType, CellMatrix.InitCellMatrix(cellType));
                    cells.Add(cell);
                }
            }
            return cells;
        }

        private static (CellType, SpecialCellType) InitPipeType(char[,] map, int x, int y)
        {
            return Enum.TryParse(MapMapper[map[x, y]], out CellType cellType) ? (cellType, SpecialCellType.None) : (CellType.LineCell, Enum.Parse<SpecialCellType>(MapMapper[map[x, y]]));
        }
    }
}