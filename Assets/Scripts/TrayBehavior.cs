using System.Collections.Generic;
using UnityEngine;

public class TrayBehavior : MonoBehaviour {

    //AudioSource audio;

    private List<GameObject> items;

	// Use this for initialization
	void Start () {
        //audio = GetComponent<AudioSource>();
        items = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addItem(GameObject item) {
        items.Add(item);
        Debug.Log("Item added: " + item.name);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.relativeVelocity.magnitude > 1) {
            //audio.Play();
        }
    }

    public void throwTray(Vector3 direction) {
        reactivateTrayPhysics();
        reactivatePhysicsOfTrayItems();

        applyForce(direction, this.gameObject);

    }

    /**
     * Reactivate the collider and physics for the Tray to which this script is attached.
     */
    public void reactivateTrayPhysics() {
        GameObject tray = this.gameObject;

        // Disassociate the tray from the Customer.
        tray.transform.parent = null;
        reactivatePhysics(tray);
    }

    /**
     * Reactive the colliders and physics of all items carried on this tray.
     */ 
    public void reactivatePhysicsOfTrayItems() {
        foreach (GameObject item in items) {
            reactivatePhysics(item);
        }
    }

    private void reactivatePhysics(GameObject gameObject) {
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    private void applyForce(Vector3 direction, GameObject gameObject) {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * 20, ForceMode.Impulse);
        rigidbody.AddTorque(gameObject.transform.right * 10);
    }

}
