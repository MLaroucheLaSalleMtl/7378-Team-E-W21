using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private List<GameObject> keyList;
    private bool unlocked;
    private int i;

    public bool Unlocked { get => unlocked; set => unlocked = value; }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        CheckKey();
    }


    private void CheckKey()
    {
        i = keyList.Count;
        foreach (GameObject gObj in keyList)
        {
            if (gObj.GetComponent<CylinderKeyDemo>().HasKey == true)
            {
                i--;
            }
        }
        if (i == 0)
        {
            Unlocked = true;
        }
        else
        {
            Unlocked = false;
        }
    }
}
