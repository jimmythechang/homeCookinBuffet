using UnityEngine;

/**
 * A class for things that should be cleaned up.
 */
public class Deletable : MonoBehaviour {

    private int lifetime = 30;

    public void flagForDeletion() {
        Destroy(gameObject, lifetime);
    }
}
