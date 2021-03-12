using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentDemo : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] List<GameObject> crystals;
    [SerializeField] Material mat;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in crystals)
        {
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        wall.SetActive(false);
        foreach (GameObject go in crystals)
        {
            go.SetActive(true);
        }
        GetComponent<MeshRenderer>().material = mat;
    }
}
