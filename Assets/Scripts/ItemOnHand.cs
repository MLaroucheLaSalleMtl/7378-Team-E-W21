using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnHand : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemArr;
    private int itemRoll;
    private bool hasItem;

    // Start is called before the first frame update
    void Start()
    {
        itemRoll = 0;
        foreach (GameObject obj in itemArr)
        {
            itemRoll++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hasItem = GetComponentInParent<PickUp>().hasItem;
    }

    public void holdItem(int itemNum)
    {
        if (hasItem)
        {
            foreach (GameObject obj in itemArr)
            {
                obj.SetActive(false);
            }

            for (int i = 0; i < itemRoll; i++)
            {
                if (i == itemNum)
                {
                    itemArr[i].SetActive(true);
                }
            }
        }
        else
        {
            foreach (GameObject obj in itemArr)
            {
                obj.SetActive(false);
            }
        }
    }
}
