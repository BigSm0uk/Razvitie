using System.Linq;

namespace Model.LevelsData
{
    public static class LevelsData
    {
        public static char[,] Map;

        public static void Init(string map)
        {
            var matrix = map.Split(" ").Select(x => x.ToCharArray()).ToArray();
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    Map[i, j] = matrix[i][j];
                }
            }
        }

        public static void Init(int gameLevel)
        {
            Map = gameLevel switch
            {
                1 => new[,]
                {
                    {'I','C','N','N','N','N','N','C','E'},
                    {'N','L','C','C','N','N','N','P','N'},
                    {'N','C','C','P','C','N','C','C','N'},
                    {'N','N','N','N','C','L','C','N','N'},
                },
                2 => new[,]
                {
                    {'I','C','N','N','N','C','L','C','N','C','E'},
                    {'N','L','N','N','C','C','N','L','C','C','N'},
                    {'C','C','C','C','C','C','N','C','P','N','N'},
                    {'C','L','C','P','N','L','N','N','N','N','N'},
                    {'N','N','N','C','L','C','N','N','N','N','N'}
                },
                3 => new[,]
                {
                    {'I','C','N','N','N','N','N','N','N','N','C','E'},
                    {'N','L','N','P','L','C','N','N','N','C','C','N'},
                    {'N','L','C','C','C','C','N','N','C','C','N','N'},
                    {'N','C','C','N','C','C','N','C','P','N','N','N'},
                    {'N','N','N','N','N','L','C','C','N','N','N','N'},
                    {'N','N','N','N','N','C','C','N','N','N','N','N'},
                },
                _ => Map
            };
        }
    }
}