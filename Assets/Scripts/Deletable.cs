using UnityEngine;

/**
 *
 */
public class Deletable : MonoBehaviour {

    private int lifetime = 10;

    public void flagForDeletion() {
        Destroy(gameObject, lifetime);
    }
}
