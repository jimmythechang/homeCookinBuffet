using System.Collections.Generic;
using UnityEngine;

/**
 * Food goes here!
 */
public class Plate : Deletable {

    private bool initializing;

    private bool withinTrayZone;

    // A reference to the single Tray held by the Player.
    private Tray heldTray;
    public Tray HeldTray {
        get { return heldTray; }
        set { heldTray = value; }
    }

    private List<Food> food;

    private void Start() {
        initializing = true;
        withinTrayZone = false;
    }

    /**
     * Determine if the Plate has entered a Tray Zone.
     */
    private void OnTriggerEnter(Collider other) {
        if (initializing && other.gameObject.GetComponent<Tray>() != null) {
            withinTrayZone = true;
            heldTray.addItem(gameObject);
        }
    }

    /**
     * If a Plate falls out of the Tray Zone, remove it from 
     * the Tray's list of items and mark it for deletion.
     */
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<Tray>() != null) {
            heldTray.removeFromItems(this.gameObject);
            gameObject.transform.SetParent(null);
            flagForDeletion();
        }
    }

    /**
     * Anchor the Plate to the general mass of the Tray.
     */
    private void OnCollisionEnter(Collision collision) {
        if (initializing && withinTrayZone) {           
            gameObject.transform.SetParent(heldTray.transform);
            initializing = false;
        }        
    }
}
