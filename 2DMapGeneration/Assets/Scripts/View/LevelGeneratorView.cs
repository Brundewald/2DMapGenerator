using UnityEngine;
using UnityEngine.Tilemaps;

namespace View
{
    public class LevelGeneratorView : MonoBehaviour
    {
        [SerializeField] private Tilemap _tileMapGround;
        [SerializeField] private Tile _tileGround;
        [SerializeField] private int _widthMap;
        [SerializeField] private int _heightMap;
        [SerializeField] private int _smoothFactor;
        [SerializeField] [Range(0, 100)] private int _randomFillPercent;

        public Tilemap TileMapGround => _tileMapGround;
        public Tile TileGround => _tileGround;
        public int WidthMap => _widthMap;
        public int HeightMap => _heightMap;
        public int SmoothFactor => _smoothFactor;
        public int RandomFillPercent => _randomFillPercent;
    }   
}
