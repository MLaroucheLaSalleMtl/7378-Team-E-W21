using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private SphereCollider sphCollider;
    private Rigidbody rb;
    private Light auraLight;
    private bool pickedUp;
    private bool playerNear;

    public bool PickedUp { get => pickedUp; set => pickedUp = value; }

    // Start is called before the first frame update
    void Start()
    {
        sphCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        auraLight = GetComponent<Light>();
        auraLight.enabled = false;
        playerNear = false;
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear)
        {
            auraLight.enabled = true;
        }
        else
        {
            auraLight.enabled = false;
        }

        if (pickedUp)
        {
            sphCollider.enabled = false;
            auraLight.enabled = true;
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        else
        {
            sphCollider.enabled = true;
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNear = false;
        }
    }

}
