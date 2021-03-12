using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpProps : MonoBehaviour
{
    //手上是否有道具了
    public bool is_have_a_prop;
    //道具
    public Transform props;
    //右手
    public Transform right_hand;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (props != null && !is_have_a_prop)
            {
                //手上就是有道具了
                is_have_a_prop = true;
                //调用水晶类的拿起道具函数
                props.GetComponent<Crystal>().setPickUp(right_hand);
            }
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "Crystal")
        {
            //触发到水晶
            props = collider.transform;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "Crystal")
        {
            //触发到水晶
            props = null;
        }
    }
}
