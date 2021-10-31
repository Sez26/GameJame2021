using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMaker : MonoBehaviour
{
    public float FOV;
    public int rayCount;
    public float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        float angle = 0.0f;
        float step = FOV / rayCount;

        Vector3 position = Vector3.zero;
        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;
        int vidx = 1;
        int tidx = 0;

        for(int i = 0; i < rayCount; i++)
        {
            Vector3 vertex = position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * maxDistance;
            vertices[vidx] = vertex;
            uv[vidx] = new Vector2(vertex.x, vertex.y);
            Debug.Log(vertex);
            if (i > 0)
            {
                triangles[tidx] = 0;
                triangles[tidx + 1] = vidx - 1;
                triangles[tidx + 2] = vidx;
                tidx += 3;
            }
            angle -= step;

            mesh.RecalculateNormals();
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
