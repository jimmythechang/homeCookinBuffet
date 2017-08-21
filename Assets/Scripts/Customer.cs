using UnityEngine;

/**
 * You're hungry.
 */
public class Customer : MonoBehaviour {

    public enum State { EMPTY_HANDED, HOLDING_ITEM }
    private State currentState = State.EMPTY_HANDED;
    public State CurrentState {
        get { return currentState; }
        set { currentState = value; }
    }

    private DefaultMouseBehavior defaultMouseBehavior = new DefaultMouseBehavior();

    private Holdable heldItem = null;
    public Holdable HeldItem {
        get { return heldItem; }
        set { heldItem = value; }
    }

	void Update () {
        // TODO: create a state machine for the Customer out of multiple scripts.
        switch (currentState) {
            case State.EMPTY_HANDED:
                // Determine if the user has clicked on anything within a certain radius. 
                if (Input.GetMouseButtonDown(0)) {
                   defaultMouseBehavior.leftClick(this);
                }
                break;
            case State.HOLDING_ITEM:
                if (Input.GetMouseButtonDown(0)) {
                    heldItem.leftClick(this);
                }
                else if (Input.GetMouseButtonDown(1)) {
                    heldItem.rightClick(this);
                }

                break;
        }
	}
}
