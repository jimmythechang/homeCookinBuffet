using UnityEngine;

/**
 * Grab yourself a tray.
 */
public class TrayGenerator : MonoBehaviour, Clickable {

    public GameObject tray;

    /*
     * Variables for tray position.
     */
    private readonly float TRAY_DISTANCE = 0.5f;
    private readonly float TRAY_HEIGHT = 0.3f;

    /**
     * Instantiates a tray before a Customer.
     */
    public void onClick(Customer customer) {
        if (!customer.isCarryingTray()) {
            Transform customerTransform = customer.transform;

            Vector3 trayPosition = customerTransform.position;

            trayPosition += customerTransform.forward * TRAY_DISTANCE;
            trayPosition.y += TRAY_HEIGHT;

            customer.Tray = Instantiate(tray, trayPosition, customerTransform.rotation, customerTransform);
        }
    }
}
