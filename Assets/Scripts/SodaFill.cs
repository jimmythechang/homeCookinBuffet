using UnityEngine;

/**
 * A class for modifying a specific mesh representing soda in a glass.
 */
public class SodaFill : MonoBehaviour {

    private float oldFillPercentage = 0;
    public float currentFillPercentage;

    private Mesh mesh;
    private Vector3[] originalVertices;

    private float glassRatio = 0.60485f;
    private float glassHeight = 3;

    // A very particular list of vertices we want to modify in the mesh.
    private int[] pointsToAlter = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 16, 19, 20, 23, 24, 27, 28, 31, 32, 35, 36, 39, 40, 43, 44, 47};

	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;

        // Persist the coordinates of the original vertices.  
        originalVertices = mesh.vertices;

        // The mesh is hidden at the start.
        GetComponent<MeshRenderer>().enabled = false;
	}
	
	void Update () {
        if (oldFillPercentage != currentFillPercentage) {
            updateMesh();
            raiseSphereCollider();
        }
    }

    /**
     * Updates the soda mesh to simulate the climb of the soda up the glass.
     */
    private void updateMesh() {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        if (currentFillPercentage != 0) {
            GetComponent<MeshRenderer>().enabled = true;
        }

        if (currentFillPercentage > 100) {
            currentFillPercentage = 100;
        }

        // Scale each vertex accordingly.
        for (int i = 0; i < pointsToAlter.Length; i++) {
            int index = pointsToAlter[i];
            vertices[index] = alterVertex(index, vertices[index]);
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        oldFillPercentage = currentFillPercentage;
    }

    /**
     * Raises the sphere collider responsible for interacting with fluid particles.
     */
    private void raiseSphereCollider() {
        SphereCollider collider = GetComponent<SphereCollider>();
        Vector3 colliderPosition = collider.transform.position;
        collider.transform.position.Set(colliderPosition.x, glassHeight * currentFillPercentage / 100f, colliderPosition.z);
    }

    /**
     * Scales a given vertex accordingly
     */
    private Vector3 alterVertex(int index, Vector3 vertex) {
        vertex.x = (glassRatio * (currentFillPercentage / 100f) + 1) * originalVertices[index].x;
        vertex.z = (glassRatio * (currentFillPercentage / 100f) + 1) * originalVertices[index].z;

        vertex.y = glassHeight * currentFillPercentage / 100f;
        return new Vector3(vertex.x, vertex.y, vertex.z);
    }

    /**
     * TODO: only call whenever a soda particle enters the glass.
     */
    void OnParticleCollision(GameObject particle) {
        currentFillPercentage += 0.5f;
    }
        
    
}
