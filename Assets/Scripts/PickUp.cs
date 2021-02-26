using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private SphereCollider sphereCollider;
    public bool hasItem = false;

    private GameObject collideWith = null;
    private GameObject itemOnHand;
    [SerializeField] private GameObject handSlot;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DropItem();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hasItem && collideWith != null)
            {
                hasItem = true;
                itemOnHand = collideWith;
                collideWith = null;

                itemOnHand.transform.parent = handSlot.transform;
                itemOnHand.transform.localPosition = new Vector3(0, 0, 0);
                itemOnHand.transform.localRotation = Quaternion.Euler(0, 0, 0);
                itemOnHand.GetComponent<PickUpItem>().PickedUp = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            if (!hasItem && collideWith == null)
            {
                collideWith = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            if (!hasItem && collideWith != null)
            {
                collideWith = null;
            }
        }
    }

    private void DropItem()
    {
        if (hasItem)
        {
            hasItem = false;
            itemOnHand.transform.parent = null;
            itemOnHand.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            itemOnHand.GetComponent<PickUpItem>().PickedUp = false;
            itemOnHand = null;
        }
    }

}
