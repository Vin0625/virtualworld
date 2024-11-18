using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CurvedRoadMesh : BaseMakeMesh
{
    protected override void SetVertices(){
        float angleStep = 90.0f / dense;
        for (int i = 0; i <= dense; i++)
        {
            float angleInRad = Mathf.Deg2Rad * (angleStep * i);
            float x = Mathf.Cos(angleInRad) * size;
            float z = Mathf.Sin(angleInRad) * size;
            vertices.Add(new Vector3(x, 0f, z));
            x = Mathf.Cos(angleInRad) * hsize;
            z = Mathf.Sin(angleInRad) * hsize;
            vertices.Add(new Vector3(x, 0f, z));
            
        }
    }

    protected override void SetNormals(){
        for (int i = 0; i < vertices.Count; i++)
        {
            normals.Add(new Vector3(0f, 1f, 0f));
        }
    }

    protected override void SetUV(){
        for (int i = 0; i <= dense; i++)
        {
            uv.Add(new Vector2((float)i / (float)dense, 1));
            uv.Add(new Vector2((float)i / (float)dense, 0));
        }
    }

    protected override void SetTriangles(){
        for (int i = 0; i < dense * 2; i += 2)
    {
        triangles.Add(i);
        triangles.Add(i + 1);
        triangles.Add(i + 2);

        triangles.Add(i + 2);
        triangles.Add(i + 1);
        triangles.Add(i + 3);
    }
    }
}
