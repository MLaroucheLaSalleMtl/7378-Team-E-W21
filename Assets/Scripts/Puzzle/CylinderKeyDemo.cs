using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderKeyDemo : MonoBehaviour
{
    [SerializeField] private GameObject keySlot;
    private bool hasKey;

    public bool HasKey { get => hasKey; set => hasKey = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keySlot.GetComponentInChildren<PickUpItem>() != null)
        {
            hasKey = true;
        }
        else
        {
            hasKey = false;
        }
    }
}
