using UnityEngine;

public class DefaultMouseBehavior {

    private readonly float CLICK_DISTANCE = 3.0f;

    /**
     * Interact with whatever the player clicks on.
     */
    public void leftClick(Customer customer) {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, CLICK_DISTANCE)) {
            hit.transform.gameObject.SendMessage("onClick", customer);
        }
    }

    /**
     * No-op for now.
     */
    public void rightClick() {

    }
}
