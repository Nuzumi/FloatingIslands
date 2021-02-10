using UnityEngine;

namespace Terrain
{
    public class TerrainSmoothnessController 
    {
        private readonly TerrainSettings settings;

        public TerrainSmoothnessController(TerrainSettings settings)
        {
            this.settings = settings;
        }

        public float GetSmoothValue(float height)
        {
            float heightOnScale = Mathf.InverseLerp(settings.Min, settings.Max, height);
            float smoothnessValue = settings.Curve.Evaluate(heightOnScale);
            return height * smoothnessValue;
        }
    }
}
