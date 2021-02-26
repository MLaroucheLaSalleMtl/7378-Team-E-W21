using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private float hp;

    public float Hp { get => hp; set => hp = value; }

    public player()
    {
        hp = Hp;
    }
    
    public player(int hp)
    {
        hp = 100;
    }
}
