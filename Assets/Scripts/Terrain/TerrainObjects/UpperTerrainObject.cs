using UnityEngine;

namespace Terrain.TerrainObjects
{
    public class UpperTerrainObject : TerrainObject
    {
        protected override int Modifier => Settings.UpperModifier;

        protected override Gradient Gradient => Settings.UpperGradient;

        protected override int[] CreateTriangles()
        {
            int[] triangles = new int[(Settings.Resolution - 1) * (Settings.Resolution - 1) * 6];
            int trianglesIndex = 0;
            for (int y = 0; y < Settings.Resolution - 1; y++)
            {
                for (int x = 0; x < Settings.Resolution - 1; x++)
                {
                    int i = x + y * Settings.Resolution;

                    triangles[trianglesIndex] = i;
                    triangles[trianglesIndex + 1] = i + Settings.Resolution;
                    triangles[trianglesIndex + 2] = i + Settings.Resolution + 1;

                    triangles[trianglesIndex + 3] = i;
                    triangles[trianglesIndex + 4] = i + Settings.Resolution + 1;
                    triangles[trianglesIndex + 5] = i + 1;
                    trianglesIndex += 6;
                }
            }

            return triangles;
        }
    }
}
