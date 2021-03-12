using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private SphereCollider sphereCollider;
    public bool hasItem = false;

    private GameObject collideWith = null;
    private GameObject itemOnHand;
    private GameObject puzzle;
    private GameObject monument;
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
            else if (hasItem && puzzle != null)
            {
                hasItem = false;
                itemOnHand.transform.parent = puzzle.transform.GetChild(0);
                //itemOnHand.transform.localPosition = Vector3.Lerp(transform.position, transform.parent.position, 1);
                itemOnHand.transform.localPosition = new Vector3(0, 0, 0);
                itemOnHand.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (monument != null)
            {
                monument.GetComponent<MonumentDemo>().Activate();
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
        
        if (other.tag == "Cylinder")
        {
            bool hasKey = other.GetComponent<CylinderKeyDemo>().HasKey;
            if (hasItem && !hasKey)
            {
                puzzle = other.gameObject;
            }
        }

        if (other.tag == "Monument")
        {
            monument = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            if (collideWith != null)
            {
                collideWith = null;
            }
        }

        if (other.tag == "Cylinder")
        {
            if (puzzle != null)
            {
                puzzle = null;
            }
        }

        if (other.tag == "Monument")
        {
            if (monument != null)
            {
                monument = null;
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
