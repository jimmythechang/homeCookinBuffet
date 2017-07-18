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
            customer.HeldItem.GetComponent<Tray>() != null) {

            Tray tray = customer.HeldItem.GetComponent<Tray>();

            // TODO: consider adding an upper limit.

            Transform trayTransform = customer.HeldItem.transform;
            Vector3 aboveTray = trayTransform.position + (Vector3.up * 0.3f) + (Vector3.back * .1f);
            
            GameObject plateObject = Instantiate(plate, aboveTray, trayTransform.rotation, trayTransform);

            tray.addItem(plateObject);
        }
    }
}
