using System.Collections.Generic;
using UnityEngine;

/**
 * Gotta carry all that food back somehow, right?
 */
public class Tray : Deletable, Holdable {

    private List<GameObject> items;

    public int maxItems = 10;

    private DefaultMouseBehavior defaultMouseBehavior;

    public static readonly string ACTIVE_TRAY_TAG = "HeldTray";

	void Start () {
        items = new List<GameObject>();
        defaultMouseBehavior = new DefaultMouseBehavior();
        tag = ACTIVE_TRAY_TAG;
    }

    /**
     * Add an item to this Tray.
     * 
     * <param name="item">The item to be added.</param>
     */
    public void addItem(GameObject item) {
        if (items.Count < maxItems) {
            items.Add(item);
        }
    }

    /**
     * Remove an item from the Tray.
     * 
     * <param name="item">The item to be removed (if it exists).</param>
     */
    public void removeFromItems(GameObject item) {
        items.Remove(item);
    }

    /**
     * Default leftClick() behavior.
     * 
     * <param name="customer">The Customer who clicked.</param>
     */
    public void leftClick(Customer customer) {
        defaultMouseBehavior.leftClick(customer);
    }

    /**
     * Throw the Tray away.
     * 
     * <param name="customer">The Customer who clicked.</param>
     */
    public void rightClick(Customer customer) {
        tag = "Untagged";

        throwTray(Camera.main.transform.forward);
        customer.CurrentState = Customer.State.EMPTY_HANDED;
    }

    /**
     * Throw the tray and everything on it in the specified direction.
     * 
     * <param name="direction">The general Vector3 in which to throw everything.</param>
     */
    public void throwTray(Vector3 direction) {
        reactivatePhysics(this.gameObject);

        applyForce(direction, this.gameObject);
        foreach (GameObject item in items) {
            reactivatePhysics(item);
            applyForce(generateRandomVector(direction), item);
        }

        flagForDeletion();
    }

    /**
     * Reactives the collider and physics on the supplied GameObject.
     * 
     * <param name="gameObject">The GameObject to reactivate physics on.</param>
     */
    private void reactivatePhysics(GameObject gameObject) {
        gameObject.transform.parent = null;
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
    }

    /**
     * Method for applying a force on a GameObject in a given direction.
     * 
     * <param name="direction">The Vector3 direction to apply force in.</param>
     * <param name="gameObject">The GameObject to apply force on.</param>
     */
    private void applyForce(Vector3 direction, GameObject gameObject) {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * 20, ForceMode.Impulse);
        rigidbody.AddTorque(gameObject.transform.right * 10);
    }

    /**
     * Code adapted from the internet! http://answers.unity3d.com/questions/492916/shotgun-bullet-spread.html
     * 
     * Returns a randomly offset Vector3, based on 
     * a provided Vector3.
     * 
     * <param name="direction">Vector3</param>
     */
    private Vector3 generateRandomVector(Vector3 direction) {
        direction.Normalize();

        Vector3 randomVector;
        do {
            randomVector = Random.insideUnitSphere;
        }
        while (randomVector == direction || randomVector == -direction);

        /*
         * Take the cross product of the provided vector and the randomly generated vector,
         * and randomly scale the magnitude of the resulting vector.
         */
        randomVector = Vector3.Cross(direction, randomVector);
        randomVector *= Random.Range(0, 1.0f);

        /*
         * Return a new vector, the original displaced by the random.
         */
        return direction + randomVector;
    }

}
