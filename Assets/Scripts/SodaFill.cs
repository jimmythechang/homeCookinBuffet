using UnityEngine;

/**
 * A class for modifying a particular mesh representing soda.
 */
public class SodaFill : MonoBehaviour {

    public float fillPercentage;

    private Mesh mesh;
    private Vector3[] originalVertices;

	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;

        // Persist the coordinates of the original vertices.  
        originalVertices = mesh.vertices;
	}
	
	// Update is called once per frame
	void Update () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;      

        for (int i = 0; i < 8; i++) {
            vertices[i] = alterVertex(vertices[i]);
        }

        

        
        mesh.vertices = vertices;
    }

    private Vector3 alterVertex(Vector3 vertex) {
        float xSign = 1f;
        float zSign = 1f;

        
        if (vertex.x < 0) {
            xSign = -1f;
        }

        if (vertex.z < 0) {
            zSign = -1f;
        }

        vertex.x = xSign * 0.34653f * (fillPercentage / 100f) + (xSign * .572921f);
        vertex.z = zSign * 0.34653f * (fillPercentage / 100f) + (zSign * .572921f);

        vertex.y = 3 * fillPercentage / 100f;
        return new Vector3(vertex.x, vertex.y, vertex.z);
    }
   
}
