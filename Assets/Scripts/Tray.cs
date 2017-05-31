using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, Holdable {

    //AudioSource audio;

    private List<GameObject> items;

    private DefaultMouseBehavior defaultMouseBehavior;

	void Start () {
        //audio = GetComponent<AudioSource>();
        items = new List<GameObject>();
        defaultMouseBehavior = new DefaultMouseBehavior();
    }

    /**
     * Add an item to this Tray.
     * 
     * <param name="item">The item to be added.</param>
     */
    public void addItem(GameObject item) {
        items.Add(item);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude > 1) {
            //audio.Play();
        }
    }

    public void leftClick(Customer customer) {
        defaultMouseBehavior.leftClick(customer);
    }

    public void rightClick(Customer customer) {
        throwTray(Camera.main.transform.forward);
        customer.CurrentState = Customer.State.EMPTY_HANDED;
    }

    public void throwTray(Vector3 direction) {
        reactivateTrayPhysics();

        applyForce(direction, this.gameObject);
        foreach (GameObject item in items) {
            reactivatePhysics(item);
            applyForce(generateRandomVector(direction), item);
        }
    }

    /**
     * Reactivate the collider and physics for this Tray.
     */
    public void reactivateTrayPhysics() {
        GameObject tray = this.gameObject;

        // Disassociate the tray from the Customer.
        tray.transform.parent = null;
        reactivatePhysics(tray);
    }

    /**
     * Reactives the collider and physics on the supplied GameObject.
     * <param name="gameObject">The GameObject to reactivate physics on.</param>
     */
    private void reactivatePhysics(GameObject gameObject) {
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

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
