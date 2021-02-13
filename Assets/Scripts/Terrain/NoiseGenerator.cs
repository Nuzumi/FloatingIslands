using UnityEngine;

namespace Terrain
{
    
    public class NoiseGenerator 
    {
        private readonly TerrainSettings settings;


        public NoiseGenerator(TerrainSettings settings)
        {
            this.settings = settings;
        }

        public float GetNoiseValue(Vector3 chunkPosition, Vector2Int vertexPosition)
        {
            float value = 0;
            for (int i = 0; i < settings.OctaveCount; i++)
            {
                value += GetOctaveValue(chunkPosition, vertexPosition, i) * ((float)i % 2 == 0 ? -1f: 1f);
            }
            return value;
        }

        private float GetOctaveValue(Vector3 chunkPosition, Vector2Int vertexPosition, int octave)
        {
            float octaveValue = Mathf.Pow(2, octave);
            float octaveScaleValue = octaveValue * settings.Scale;
            float perlinX = GetPerlinX(chunkPosition, vertexPosition, octaveScaleValue);
            float perlinY = GetPerlinY(chunkPosition, vertexPosition, octaveScaleValue);
            return Mathf.PerlinNoise(perlinX, perlinY) / octaveValue;
        }

        private float GetPerlinY(Vector3 chunkPosition, Vector2Int vertexPosition, float octaveScaleValue)
        {
            return ((float)vertexPosition.y / (settings.Resolution - 1) + chunkPosition.z ) * octaveScaleValue  + settings.Offset.y;
        }

        private float GetPerlinX(Vector3 chunkPosition, Vector2Int vertexPosition, float octaveScaleValue)
        {
            return ((float)vertexPosition.x / (settings.Resolution - 1) + chunkPosition.x ) * octaveScaleValue + settings.Offset.x;
        }
    }
}
