using UnityEngine;
using static UnityEngine.Mesh;

namespace BroWar.Common.Utilities
{
    public static class MeshUtility
    {
        public static Mesh CreatePlaneMesh(string name = "Plane")
        {
            return new Mesh
            {
                name = name,
                vertices = new Vector3[]
                {
                    //x = 0, y = 0
                    new Vector3(-1, 0, -1),
                    //x = 0, y = 1
                    new Vector3(-1, 0, +1),
                    new Vector3(+1, 0, -1),
                    new Vector3(+1, 0, +1),
                },
                uv = new Vector2[]
                {
                    new Vector2(0, 0),
                    new Vector2(0, 1),
                    new Vector2(1, 0),
                    new Vector2(1, 1)
                },
                normals = new Vector3[]
                {
                    Vector3.up,
                    Vector3.up,
                    Vector3.up,
                    Vector3.up,
                },
                triangles = new int[] { 0, 3, 2, 3, 0, 1 },
                bounds = new Bounds(Vector3.zero, new Vector3(2, 0, 2))
            };
        }

        public static int GetVertexCount(this MeshData mesh)
        {
            return mesh.GetVertexData<Vector3>(0).Length;
        }

        public static int GetIndicesCount(this MeshData mesh)
        {
            return mesh.GetIndexData<int>().Length;
        }

        public static int GetTriangleCount(this MeshData mesh)
        {
            return mesh.GetIndicesCount() / 3;
        }
    }
}