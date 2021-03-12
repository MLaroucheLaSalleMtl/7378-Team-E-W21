using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    //是否是被拿着的、
    public bool is_n;
    //灯光
    public Light this_light;
    public Collider this_collider;
    //刚体
    public Rigidbody this_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        this_light = GetComponent<Light>();
        this_collider = GetComponent<Collider>();
        this_rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    //拿起方法
    public void setPickUp(Transform hand){
        //拿起是的
        is_n = true;
        //冻结刚体 停止物理运动
        this_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        this_collider.enabled = false;
        //把我位置 设置成拿我的对象的手
        transform.SetParent(hand);
        transform.position = hand.position;
        //开灯
        this_light.enabled = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "player" && !is_n)
        {
            this_light.enabled = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "player" && !is_n)
        {
            this_light.enabled = false;
        }
    }
}