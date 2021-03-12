using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] private Animator DoorR;
    [SerializeField] private Animator DoorL;
    private bool locked;

    void Start()
    {
        
    }

    void Update()
    {
        locked = GetComponentInParent<Puzzle>().Unlocked;
        Unlock(locked);
    }

    private void Unlock(bool unlocked)
    {
        if (unlocked)
        {
            DoorR.SetBool("Open", true);
            DoorL.SetBool("Open", true);
        }
        else
        {
            DoorR.SetBool("Open", false);
            DoorL.SetBool("Open", false);
        }
    }
}
