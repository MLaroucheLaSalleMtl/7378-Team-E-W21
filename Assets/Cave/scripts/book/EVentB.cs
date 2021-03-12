using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EVentB : MonoBehaviour
{
    //背包打开
    public GameObject knapsack_sum;
    public Animation knapsack_sum_animation;
    // Start is called before the first frame update
    void Start()
    {
        knapsack_sum_animation = knapsack_sum.transform.GetComponent<Animation>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!knapsack_sum.activeSelf)
            {
                knapsack_sum.SetActive(true);
            }
            else {
                knapsack_sum_animation.Play("book exit");
            }
        }
    }
}