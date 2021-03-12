using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class T : MonoBehaviour
{
    
    public Text this_t;
    //打的字符串
    public string type_ar;
    public float time;
    public float time_count;
    public int type_index = 0;
    public bool is_typewriter;
    // Start is called before the first frame update
    void Start()
    {
        time_count = time;

        this_t = GetComponent<Text>();
    }
    private void OnEnable()
    {
        //泰迪熊数量大于1
        if(GM.knapsack_sum >= 1)
        {
            type_index = 0;
            GetComponent<Text>().text = "";
            is_typewriter = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (is_typewriter)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = time_count;
                this_t.text = this_t.text + type_ar[type_index];
                type_index += 1;
                if(type_index >= type_ar.Length)
                {
                    is_typewriter = false;
                }
            }
        }
    }
}