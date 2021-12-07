using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMesh : MonoBehaviour
{
    //Variables
    // Set up width height and depth variables
    public float meshWidth = 10f;
    public float meshHeight = 10f;
    public float meshDepth = 10f;

    // Use this for initialization
    void Start()
    {
        // Add Meshfilter Component to GameObject
        gameObject.AddComponent<MeshFilter>();
        // Create a mesh Object
        Mesh mesh = new Mesh();
        // Attach the mesh to the mesh property of the Game Object  
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        // Add the Mesh Render to the Game Object
        gameObject.AddComponent<MeshRenderer>();

        // Vertices
        Vector3[] vertices = new Vector3[8] {

            new Vector3(0,0,0),
            new Vector3(meshWidth,0,0),
            new Vector3(0,meshHeight,0),
            new Vector3(meshWidth, meshHeight,0),

            new Vector3(0,0,meshDepth),
            new Vector3(meshWidth,0,meshDepth),
            new Vector3(0,meshHeight,meshDepth),
            new Vector3(meshWidth, meshHeight,meshDepth)

        };

        // Triangles
        int[] triangles = new int[36];
        triangles[0] = 1;
        triangles[1] = 0;
        triangles[2] = 2;

        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;


        triangles[6] = 1;
        triangles[7] = 3;
        triangles[8] = 5;

        triangles[9] = 5;
        triangles[10] = 3;
        triangles[11] = 7;


        triangles[12] = 5;
        triangles[13] = 7;
        triangles[14] = 4;

        triangles[15] = 4;
        triangles[16] = 7;
        triangles[17] = 6;


        triangles[18] = 6;
        triangles[19] = 2;
        triangles[20] = 0;

        triangles[21] = 0;
        triangles[22] = 4;
        triangles[23] = 6;


        triangles[24] = 0;
        triangles[25] = 1;
        triangles[26] = 5;

        triangles[27] = 5;
        triangles[28] = 4;
        triangles[29] = 0;


        triangles[30] = 6;
        triangles[31] = 7;
        triangles[32] = 3;

        triangles[33] = 3;
        triangles[34] = 2;
        triangles[35] = 6;

        // Update mesh with vertices, triangles and normals
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        //Fetch the Material from the Renderer of the GameObject
        Renderer goRender = gameObject.GetComponent<Renderer>();
        // Update the color of the Game Object
        goRender.material.color = Color.black;
    }
}
