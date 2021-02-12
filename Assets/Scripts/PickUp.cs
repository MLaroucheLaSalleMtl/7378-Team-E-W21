using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private SphereCollider sphereCollider;
    [SerializeField] private GameObject torch;
    private bool hasTorch = false;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTorch)
        {
            torch.SetActive(true);
        }
        else
        {
            torch.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DropItem();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    if (!hasTorch)
            //    {
            //        hasTorch = true;
            //        other.gameObject.SetActive(false);
            //    }
            //}

            if (!hasTorch)
            {
                hasTorch = true;
                other.gameObject.SetActive(false);
            }
        }

    }

    private void DropItem()
    {
        if (hasTorch)
        {
            hasTorch = false;
        }
    }

}
