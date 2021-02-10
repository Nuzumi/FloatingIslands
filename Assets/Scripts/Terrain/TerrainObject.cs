using System;
using UnityEngine;

namespace Terrain
{
    public class TerrainObject : MonoBehaviour
    {
        [SerializeField] private TerrainController controller;
        [SerializeField] private MeshFilter meshFilter;

        private TerrainChunk chunk;

        private int Resolution => controller.settings.Resolution;
        private TerrainSettings Settings => controller.settings;

        public void SetUp()
        {
            meshFilter.mesh = new Mesh();
            chunk = new TerrainChunk(Resolution, meshFilter.sharedMesh);
            chunk.ConstructMesh();
            MakePerlinTerrain();
            SmoothOutVertices();
        }

        private void MakePerlinTerrain()
        {
            Vector3[] vertices = chunk.Vertices;
            int i = 0;
            for (int y = 0; y < Resolution; y++)
            {
                for (int x = 0; x < Resolution; x++)
                {
                    float height = controller.generator.GetNoiseValue(transform.position, new Vector2Int(x, y));
                    //Settings.SetMax(height);
                    //Settings.SetMin(height);
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
                colors[i] = controller.colorController.GetColor(vertices[i].y); 
            }
            
            chunk.ChangeColor(colors);
        }

        private void SmoothOutVertices()
        {
            Vector3[] vertices = chunk.Vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].y = controller.smoothnessController.GetSmoothValue(vertices[i].y);
                Settings.SetMax(vertices[i].y);
                Settings.SetMin(vertices[i].y);
            }
            chunk.ChangeVertices(vertices);
        }
    }
}
