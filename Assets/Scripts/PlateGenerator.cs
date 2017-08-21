using UnityEngine;

public class PlateGenerator : MonoBehaviour, Clickable {

    public GameObject plate;

    /**
     * Adds a plate to the Customer's Tray, if one is held.
     * 
     * <param name="customer">The Customer.</param>
     */
    public void onClick(Customer customer) {
        if (customer.CurrentState == Customer.State.HOLDING_ITEM &&
            customer.HeldItem is Tray) {

            Tray tray = (Tray) customer.HeldItem;

            Transform trayTransform = tray.transform;
            Vector3 aboveTray = trayTransform.position + (Vector3.up * 0.3f) + (Vector3.back * .1f);
            
            Plate plateObject = Creatable.Create<Plate>(plate, aboveTray, trayTransform.rotation, trayTransform);

            // Prevent the Plate from interacting with the Player's collider when spawned/placed on the Tray.
            Physics.IgnoreCollision(plateObject.GetComponent<Collider>(), customer.GetComponent<Collider>());

            tray.addItem(plateObject.gameObject);
            plateObject.HeldTray = tray;
        }
    }
}
