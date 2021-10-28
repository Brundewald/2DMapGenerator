using UnityEngine;
using UnityEngine.Tilemaps;
using View;

namespace Controller
{
    public class LevelGeneratorController
    {
        private const int CountWall = 4;

        private Tilemap _tileMapGround;
        private Tile _tileGround;
        private int _widthMap;
        private int _heightMap;
        private int _smoothFactor;
        private int _randomFillPercent;

        private int[,] _map;

        private MarchingSquaresGenerationAlgorithm _marchingSquareGenerator = new MarchingSquaresGenerationAlgorithm();

        public LevelGeneratorController(LevelGeneratorView levelGeneratorView)
        {
            _tileMapGround = levelGeneratorView.TileMapGround;
            _tileGround = levelGeneratorView.TileGround;
            _widthMap = levelGeneratorView.WidthMap;
            _heightMap = levelGeneratorView.HeightMap;
            _smoothFactor = levelGeneratorView.SmoothFactor;
            _randomFillPercent = levelGeneratorView.RandomFillPercent;

            _map = new int[_widthMap, _heightMap];
        }

        public void Awake()
        {
            GenerateMarchingSquaresLevel();
        }
        public void Clear()
        {
            _tileMapGround.ClearAllTiles();
        }
        private void GenerateMarchingSquaresLevel()
        {
            RandomFillLevel();
        
            for (var i = 0; i < _smoothFactor; i++)
                SmoothMap();
        
            _marchingSquareGenerator.GenerateGrid(_map, 1);
            _marchingSquareGenerator.DrawTilesOnMap(_tileMapGround, _tileGround);
        }
        
        private void RandomFillLevel()
        {
            var seed = Time.time.GetHashCode();
            var random = new System.Random(seed);
        
            for(var x = 0; x < _widthMap; x++)
            {
                for (var y = 0; y < _heightMap; y++)
                {
                    if (x == 0 || x == _widthMap - 1 || y == 0 || y == _heightMap - 1)
                        _map[x, y] = 1;
                    else
                        _map[x, y] = random.Next(0, 100) < _randomFillPercent ? 1 : 0;
                }
            }
        }
        
        private void SmoothMap()
        {
            for (var x = 0; x < _widthMap; x++)
            {
                for (var y = 0; y < _heightMap; y++)
                {
                    var neighbourGroundTile = GetSurroundingGroundCount(x, y);
        
                    if (neighbourGroundTile > CountWall)
                        _map[x, y] = 1;
                    else if (neighbourGroundTile < CountWall)
                        _map[x, y] = 0;
                }
            }
        }
        
        
        private int GetSurroundingGroundCount(int x, int y)
        {
            var wallCount = 0;
        
            for (var neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
            {
                for (var neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
                {
                    if (neighbourX >=0 && neighbourX < _widthMap && neighbourY >=0 && neighbourY < _heightMap)
                    {
                        if (neighbourX != x || neighbourY != y)
                            wallCount += _map[x, y];
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }
        
            return wallCount;
        }
    }
}