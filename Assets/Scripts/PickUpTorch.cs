using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTorch : MonoBehaviour
{
    private float pickUpRange;
    private SphereCollider sphereCollider;
    [SerializeField] GameObject sphere;

    private string itemName = "torch";
    public string GetItemName()
    {
        return itemName;
    }

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        pickUpRange = sphereCollider.radius;
        sphere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sphere.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sphere.SetActive(false);
        }
    }
}
