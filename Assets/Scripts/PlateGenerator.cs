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

            // TODO: add functionality for stacking plates, and consider an upper limit.
            GameObject plateObject = Instantiate(plate, customer.HeldItem.transform);
            tray.addItem(plateObject);
        }
    }
}
