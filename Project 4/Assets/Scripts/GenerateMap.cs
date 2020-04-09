using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] public Vector2Int mapSize;
    [Space]
    [SerializeField] [Tooltip("Biomes are generated in the orde of the array.")] private BiomeSettings[] biomes;


    private TileTypes[,] tileTypes;
    FastNoise fastNoise = new FastNoise();

    [System.Serializable]
    public class BiomeSettings
    {
        public string biomeName = "New biome";
        public TileTypes tileType = TileTypes.Plains;
        public TilePrefab tilePrefab;
        [Space]
        [Header("Noise Settings")]
        [Tooltip("Generates the biome if the noise value is above or equel to this value")] public float returnValueAbove = 0;
        public FastNoise.NoiseType noiseType = FastNoise.NoiseType.PerlinFractal;
        public int octaves = 6;
        [Tooltip("Scale of the noise map.")] public float Frequency = 0.2f;
        public float gain;
        public float lacunarity;
    }

    public enum TileTypes
    {
        Plains,
        Water,
        Ocean,
        Jungle,
        Desert,
        Mountain,
        Forest
    }

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        tileTypes = new TileTypes[mapSize.x, mapSize.y];

        foreach (var biome in biomes)
        {
            GenerateBiome(biome.octaves,biome.Frequency, biome.gain, biome.lacunarity, biome.returnValueAbove, biome.noiseType, biome.tileType);
        }

        PlaceTiles();
    }

    private void PlaceTiles()
    {
        Tiles.tiles = new TileData[mapSize.x,mapSize.y];
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                foreach (var biome in biomes)
                {
                    if (tileTypes[x,y] == biome.tileType)
                    {
                        tilemap.SetTile((Vector3Int)HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), biome.tilePrefab.tile);
                        Tiles.tiles[x, y] = new TileData(HexagonCalculator.WorldToHexagonPosition(new Vector2(x, y)), biome.tilePrefab);
                    }
                }
            }
        }
    }

    private void GenerateBiome(int octaves, float frequency, float gain, float lacunarity, float returnValueAbove, FastNoise.NoiseType noiseType,  TileTypes tileType)
    {
        fastNoise.SetSeed(Random.Range(-5000,5000));
        fastNoise.SetFractalOctaves(octaves);
        fastNoise.SetFrequency(frequency);
        fastNoise.SetFractalGain(gain);
        fastNoise.SetFractalLacunarity(lacunarity);
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                switch (noiseType)
                {
                    case FastNoise.NoiseType.Value:
                        if (fastNoise.GetValue(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.ValueFractal:
                        if (fastNoise.GetValueFractal(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.Perlin:
                        if (fastNoise.GetPerlin(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.PerlinFractal:
                        if (fastNoise.GetPerlinFractal(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.Simplex:
                        if (fastNoise.GetSimplex(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.SimplexFractal:
                        if (fastNoise.GetSimplexFractal(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.Cellular:
                        if (fastNoise.GetCellular(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.WhiteNoise:
                        if (fastNoise.GetWhiteNoise(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.Cubic:
                        if (fastNoise.GetCubic(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.CubicFractal:
                        if (fastNoise.GetCubicFractal(x, y) >= returnValueAbove)
                        {
                            tileTypes[x, y] = tileType;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
