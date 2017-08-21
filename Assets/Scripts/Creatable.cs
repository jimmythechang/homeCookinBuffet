using UnityEngine;

public class Creatable : MonoBehaviour { 
    public static T Create<T>(GameObject gameObject, Vector3 position, Quaternion rotation, Transform parent) {
        return Instantiate(gameObject, position, rotation, parent).GetComponent<T>();
    }
}
