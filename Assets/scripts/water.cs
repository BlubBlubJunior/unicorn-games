using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    [SerializeField] private float waveSpeed = 1.0f;
    [SerializeField] private float waveHeight = 1.0f;

    private Vector3[] baseVector;
    private Mesh mesh;
    
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVector = mesh.vertices;
    }
    
    void Update()
    {
        Vector3[] vertices = new Vector3[baseVector.Length];
        
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVector[i];
            vertex.y += Mathf.Sin(Time.time * waveSpeed + baseVector[i].x) * waveHeight;
            vertices[i] = vertex;
        }

        mesh.vertices = vertices;
    }
}
