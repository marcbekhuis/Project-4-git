using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    [SerializeField] [Tooltip("Biomes are generated in the orde of the array.")] private BiomeSettings[] biomes;

    FastNoise fastNoise = new FastNoise();

    [System.Serializable]
    public class BiomeSettings
    {
        public string biomeName = "New biome";
        public GameData.BiomeTypes tileType = GameData.BiomeTypes.Plains;
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

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        foreach (var biome in biomes)
        {
            GenerateBiome(biome.octaves,biome.Frequency, biome.gain, biome.lacunarity, biome.returnValueAbove, biome.noiseType, biome.tileType);
        }

        PlaceTiles();
    }

    private void PlaceTiles()
    {
        for (int x = 0; x < GameData.mapSize.x; x++)
        {
            for (int y = 0; y < GameData.mapSize.y; y++)
            {
                foreach (var biome in biomes)
                {
                    if (GameData.biomes[x,y] == biome.tileType)
                    {
                        GameData.biomeTilemap.SetTile((Vector3Int)new Vector2Int(x, y), biome.tilePrefab.tiles[Random.Range(0, biome.tilePrefab.tiles.Length)]);
                        GameData.tiles[x, y] = new TileData(new Vector2Int(x, y), biome.tilePrefab, null);
                    }
                }
            }
        }
    }

    private void GenerateBiome(int octaves, float frequency, float gain, float lacunarity, float returnValueAbove, FastNoise.NoiseType noiseType, GameData.BiomeTypes tileType)
    {
        fastNoise.SetSeed(Random.Range(-5000,5000));
        fastNoise.SetFractalOctaves(octaves);
        fastNoise.SetFrequency(frequency);
        fastNoise.SetFractalGain(gain);
        fastNoise.SetFractalLacunarity(lacunarity);
        for (int x = 0; x < GameData.mapSize.x; x++)
        {
            for (int y = 0; y < GameData.mapSize.y; y++)
            {
                switch (noiseType)
                {
                    case FastNoise.NoiseType.Value:
                        if (fastNoise.GetValue(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.ValueFractal:
                        if (fastNoise.GetValueFractal(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.Perlin:
                        if (fastNoise.GetPerlin(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.PerlinFractal:
                        if (fastNoise.GetPerlinFractal(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.Simplex:
                        if (fastNoise.GetSimplex(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.SimplexFractal:
                        if (fastNoise.GetSimplexFractal(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.Cellular:
                        if (fastNoise.GetCellular(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.WhiteNoise:
                        if (fastNoise.GetWhiteNoise(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.Cubic:
                        if (fastNoise.GetCubic(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    case FastNoise.NoiseType.CubicFractal:
                        if (fastNoise.GetCubicFractal(x, y) >= returnValueAbove)
                        {
                            GameData.biomes[x, y] = tileType;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
