using UnityEngine;

public class Plate : Deletable {

    private bool initializing;

    private bool withinTrayZone;

    private Tray heldTray;

    private void Start() {
        initializing = true;
        withinTrayZone = false;

        heldTray = GameObject.FindGameObjectWithTag(Tray.ACTIVE_TRAY_TAG).GetComponent<Tray>();

        // Prevent the Plate from interacting with the Player's collider when spawned/placed on the Tray.
        Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>());
    }

    /**
     * Determine if the plate has entered a Tray Zone.
     */
    private void OnTriggerEnter(Collider other) {
        if (initializing && other.gameObject.GetComponent<Tray>() != null) {
            withinTrayZone = true;
            heldTray.addItem(gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<Tray>() != null) {
            Debug.Log("Plate marked for deletion!");
            heldTray.removeFromItems(this.gameObject);
            gameObject.transform.SetParent(null);
            flagForDeletion();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (initializing && withinTrayZone) {           
            gameObject.transform.SetParent(heldTray.transform);
            initializing = false;
        }        
    }
}
