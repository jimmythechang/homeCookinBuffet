using UnityEngine;

public class PlateGenerator : MonoBehaviour, Clickable {

    public GameObject plate;


    public void onClick(Customer customer) {
        if (customer.isCarryingTray()) {
            TrayBehavior trayBehavior = customer.Tray.GetComponent<TrayBehavior>();

            GameObject plateObject = Instantiate(plate, customer.Tray.transform);
            trayBehavior.addItem(plateObject);
        }
    }
}
