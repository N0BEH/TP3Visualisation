using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class meshGenerator : MonoBehaviour
{
    private Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    private MeshFilter meshFilter;

    public int size = 10;
    

    void Start()
    {
        Application.targetFrameRate = 60;
        
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; 
        CreateShape();
        UpdateMesh();

    }
    
    void CreateShape()
    {
        vertices = new Vector3[(size + 1) * (size + 1)];

        
        for (int i = 0, z =0; z<= size; z++)
        {
            for (int x = 0; x<=size; x++)
            {
                float y = 0;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[size * size * 6];

        /*
       int vert = 0;
        int tris = 0;

        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size + 1;
                triangles[tris + 5] = vert + size + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }*/

    }
    
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void OnGUI()
		{
            int labelSpacing = 44;

            Rect rect = new Rect(Screen.width - 172, 2, 170, 40);

            if (GUI.Button(rect, "Quitter"))
            {
                QuitGame();
            }
            
        }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
}