using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MakeMeshReadable : MonoBehaviour
{
    public bool markReadable = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        mesh.UploadMeshData(markReadable);
    }
}
