using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookExit : MonoBehaviour
{
    public GameObject book;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setClose()
    {
        book.SetActive(false);
    }
}
