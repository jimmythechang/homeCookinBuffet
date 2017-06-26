using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaDispenser : MonoBehaviour, Clickable {

    public GameObject glass;

    private ParticleSystem sodaStream;

    /*
     * Variables for glass position.
     */
    private readonly float GLASS_DISTANCE = 1f;
    private readonly float GLASS_HEIGHT = 0.2f;

	void Start () {
        sodaStream = this.gameObject.GetComponentInChildren<ParticleSystem>();
        sodaStream.Stop();
	}

    /**
     * Hands the Customer a glass, if they're not holding anything already.
     * 
     * <param name="customer">The Customer.</param>
     */
    public void onClick(Customer customer) {
        if (customer.CurrentState == Customer.State.EMPTY_HANDED) {
            Transform customerTransform = customer.transform;
            Vector3 glassPosition = customerTransform.position;

            glassPosition += customerTransform.forward * GLASS_DISTANCE;
            glassPosition.y += GLASS_HEIGHT;

            customer.HeldItem = Instantiate(glass, glassPosition, customerTransform.rotation, customerTransform);
            customer.CurrentState = Customer.State.HOLDING_ITEM;
        }
    }

    /**
     * Start the soda stream on trigger enter.
     * 
     * <param name="other">The triggering Collider.</param>
     */
    private void OnTriggerEnter(Collider other) {
        sodaStream.Play();
    }

    /**
     * Stop the soda stream on trigger exit.
     * 
     * <param name="other">The triggering Collider.</param>
     */
    private void OnTriggerExit(Collider other) {
        sodaStream.Stop();
    }

    void OnParticleCollision(GameObject particle) {
        Debug.Log(particle.name);
    }
}
