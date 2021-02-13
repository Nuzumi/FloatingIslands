using UnityEngine;

namespace Terrain.TerrainObjects
{
    public abstract class TerrainObject : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        private TerrainChunk chunk;
        private TerrainController terrainController;

        private int Resolution => terrainController.settings.Resolution;
        protected TerrainSettings Settings => terrainController.settings;

        protected abstract int Modifier { get; }
        protected abstract Gradient Gradient { get; }

        public void SetUp(TerrainController terrainController)
        {
            this.terrainController = terrainController;
            meshFilter.mesh = new Mesh();
            chunk = new TerrainChunk(Resolution, meshFilter.sharedMesh);
            chunk.ConstructMesh();
            chunk.ChangeTriangles(CreateTriangles());
            MakePerlinTerrain();
        }

        private void MakePerlinTerrain()
        {
            Vector3[] vertices = chunk.Vertices;
            int i = 0;
            for (int y = 0; y < Resolution; y++)
            {
                for (int x = 0; x < Resolution; x++)
                {
                    float height = terrainController.generator.GetNoiseValue(transform.position, new Vector2Int(x, y));
                    height = Mathf.Max(Settings.MinLevel, height);
                    height *= Modifier;
                    Settings.SetMax(height);
                    Settings.SetMin(height);
                    vertices[i].y = height;
                    i++;
                }
            }
            
            chunk.ChangeVertices(vertices);
        }

        public void ColorVertices()
        {
            Vector3[] vertices = chunk.Vertices;
            Color[] colors = new Color[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                colors[i] = GetColor(vertices[i].y); 
            }
            
            chunk.ChangeColor(colors);
        }

        public void SmoothOutVertices()
        {
            Vector3[] vertices = chunk.Vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                float height = terrainController.smoothnessController.GetSmoothValue(vertices[i].y);
                vertices[i].y = height;
                Settings.SetMax(height);
                Settings.SetMin(height);
            }
            chunk.ChangeVertices(vertices);
        }

        private Color GetColor(float terrainHeight)
        {
            return Gradient.Evaluate(Mathf.InverseLerp(Settings.Min, Settings.Max, terrainHeight));
        }

        protected abstract int[] CreateTriangles();
    }
}
