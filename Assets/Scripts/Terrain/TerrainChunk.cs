using UnityEngine;

namespace Terrain
{
    public class TerrainChunk
    {
        public int Resolution { get; }
        public Vector3[] Vertices => mesh.vertices;

        private readonly Mesh mesh;
        
        public TerrainChunk(int resolution, Mesh mesh)
        {
            Resolution = resolution;
            this.mesh = mesh;
        }

        public void ConstructMesh()
        {
            var vertices = new Vector3[Resolution * Resolution];
            var triangles = new int[(Resolution - 1) * (Resolution - 1) * 6];
            var trianglesIndex = 0;

            for (int y = 0; y < Resolution; y++)
            {
                for (int x = 0; x < Resolution; x++)
                {
                    int i = x + y * Resolution;
                    CreateVertice(x, y, i);
                    
                    if (x == Resolution - 1 || y == Resolution - 1)
                        continue;
                    
                    CreateTriangles(i);
                }
            }
            
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            void CreateTriangles(int i)
            {
                triangles[trianglesIndex] = i;
                triangles[trianglesIndex + 1] = i + Resolution;
                triangles[trianglesIndex + 2] = i + Resolution + 1;

                triangles[trianglesIndex + 3] = i;
                triangles[trianglesIndex + 4] = i + Resolution + 1;
                triangles[trianglesIndex + 5] = i + 1;
                trianglesIndex += 6;
            }

            void CreateVertice(int x, int y, int i)
            {
                Vector2 percent = new Vector2(x, y) / (Resolution - 1);
                Vector3 pointOnMesh = (percent.x - .5f) * Vector3.right + (percent.y - .5f) * Vector3.forward;
                vertices[i] = pointOnMesh;
            }
        }

        public void ChangeVertices(Vector3[] vertices)
        {
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
        }

        public void ChangeColor(Color[] colors)
        {
            mesh.colors = colors;
        }
    }
}
