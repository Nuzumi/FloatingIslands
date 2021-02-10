using UnityEngine;

namespace Terrain
{
    public class ColorController 
    {
        private readonly TerrainSettings settings;

        public ColorController(TerrainSettings settings)
        {
            this.settings = settings;
        }

        public Color GetColor(float terrainHeight)
        {
            var color = settings.Gradient.Evaluate(Mathf.InverseLerp(settings.Min, settings.Max, terrainHeight));
            if (terrainHeight <= settings.MinLevel)
            {
                color.a = 0;
            }
            return color;
        }
    }
}
