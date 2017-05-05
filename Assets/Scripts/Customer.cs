using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Functionality and behavior particular to a Customer.
 */
public class Customer : MonoBehaviour {

    private bool isCarryingTray;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast (ray, out hit)) {
                hit.transform.gameObject.SendMessage("onClick");
            }
        }
	}
}
