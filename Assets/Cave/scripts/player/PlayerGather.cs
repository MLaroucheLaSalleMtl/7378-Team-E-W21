using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class PlayerGather : MonoBehaviour
{
    public Transform ray_p;//射线点
    public GameObject get_tips;//提示框
    public Text teddy_bear_number;//泰迪熊背包数量text
    public GameObject teddy_bear = null;//临时存放泰迪熊的对象
    //射线收集物体
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //射线检测 获取道具
        //从摄像机发出到点击坐标的射线Input.mousePosition
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //从摄像机向屏幕中心发出射线
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point);//划出射线，在scene视图中能看到由摄像机发射出的射线
            if (hitInfo.collider.tag == "teddy bear")//当射线碰撞目标的name包含Cube，执行拾取操作
            {
                if (!get_tips.activeSelf) get_tips.SetActive(true);
                if (teddy_bear == null) teddy_bear = hitInfo.collider.gameObject;
            }
            else {
                if (teddy_bear != null) teddy_bear = null;
                if (get_tips.activeSelf) get_tips.SetActive(false);
            }
        }
        else
        {
            if (teddy_bear != null) teddy_bear = null;
            if (get_tips.activeSelf) get_tips.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (teddy_bear != null) {
                //如果泰迪熊不是null
                GM.knapsack_sum += 1;
                //预留写法 现在是删掉泰迪熊
                Destroy(teddy_bear);

                teddy_bear_number.text = "X " + GM.knapsack_sum;
            }
        }
    }
}