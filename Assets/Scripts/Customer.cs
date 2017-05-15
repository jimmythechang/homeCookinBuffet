using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * You're hungry.
 */
public class Customer : MonoBehaviour {
    private GameObject tray = null;
    public GameObject Tray {
        get { return tray; }
        set { tray = value; }
    }

    public bool isCarryingTray() {
        return tray != null;
    }


    private float clickDistance = 3.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        // Determine if the user has clicked on anything within a certain radius. 
		if (Input.GetMouseButtonDown(0)) {
            interact();
        }
        else if (Input.GetMouseButtonDown(1)) {
            throwTray();
        }
	}

    private void FixedUpdate() {
    }

    /**
     * Interact with whatever the Customer clicks on.
     */
    private void interact() {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, clickDistance)) {
            hit.transform.gameObject.SendMessage("onClick", this);
        }
    }

    /**
     * Throw the tray in disgust.
     */
    private void throwTray() {
        if (isCarryingTray()) {
            TrayBehavior trayBehavior = tray.GetComponent<TrayBehavior>();

            trayBehavior.throwTray(Camera.main.transform.forward);

            tray = null;
        }

    }

    public void applyForce(GameObject gameObject) {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(Camera.main.transform.forward * 20, ForceMode.Impulse);
        rigidbody.AddTorque(gameObject.transform.right * 10);
    }



}
