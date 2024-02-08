using System;
using System.Collections.Generic;

namespace Model.Core
{
    [Serializable]
    public enum CellType
    {
        LineCell,
        PlusCell,
        CurveCell,
        None
    }
    [Serializable]
    public enum SpecialCellType
    {
        InitCell,
        EndCell,
        None
    }

    public static class CellMatrix
    {
        private static readonly List<int> LineCellMatrix = new (9)
            {
                0,0,0,
                1,1,1,
                0,0,0,
            };

        private static readonly List<int> PlusCellMatrix = new (9)
        {
            0,1,0,
            1,1,1,
            0,1,0,
        };

        private static readonly List<int> CurveCellMatrix = new (9)
        {
            0,0,0,
            1,1,0,
            0,1,0,
        };



        public static List<int> InitCellMatrix(CellType cellType)
        {
            return cellType switch
            {
                CellType.LineCell => LineCellMatrix,
                CellType.PlusCell => PlusCellMatrix,
                CellType.CurveCell => CurveCellMatrix,
                CellType.None => null,
                _ => throw new ArgumentOutOfRangeException(nameof(cellType), cellType, null)
            };
        }
    }
    
}