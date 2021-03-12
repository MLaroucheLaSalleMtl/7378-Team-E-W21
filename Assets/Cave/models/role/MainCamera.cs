using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Camera this_camera;//摄像机，是本物体下的子物体

    public bool isAiming;//在瞄准吗

    public GameObject viewpoint;//看向的物体
    public Transform viewpoint_t;
    public Vector3 offset;//每一次移动的值
    public float smooth = 1;//摄像机移动的速度
    [Header("鼠标灵敏度")]
    public float rotate_x_speed = 1;//鼠标移动改变视角速度
    [Header("跟随速度")]
    public float moveSpeed = 5;
    [Header("摄像机往上的角度")]
    public float minAngle = -40;
    [Header("摄像机往下的角度")]
    public float maxAngle = 30;

    public float localOffsetSpeed = 8;//控制相机与父物体偏移时的平滑度
    public float localOffsetAim = 2;//根据是否瞄准而产生的偏移量，表示瞄准时摄像机应该前进多远距离，根据需要设值
    private float localOffsetAngle = 0;//根据垂直视角角度而产生的偏移量
    public float localOffsetAngleUp = 1.5f;//根据向上的角度而产生的偏移量的最大值
    public float localOffsetAngleDown = 1.5f;//根据向下的角度而产生的偏移量的最大值
    private float localOffsetCollider = 0;//根据玩家与摄像机间是否有遮挡而产生的偏移量

    public float this_camera_r;
    public Ray ray;//摄像机射线值

    // Use this for initialization
    void Start()
    {
        setStart();
    }
    public void setStart()
    {
        viewpoint_t = viewpoint.transform;
        offset = viewpoint_t.position - transform.position;//设置初始值
    }
    void LateUpdate()
    {
        //隐藏鼠标
        Cursor.lockState = CursorLockMode.Locked;//隐藏鼠标指针

        // Cursor.lockState = CursorLockMode.None;//显示鼠标指针
        //摄像机跟随移动
        transform.position = Vector3.Slerp(transform.position, viewpoint_t.position - offset, moveSpeed);

        //摄像机鼠标旋转
        float mouseX = Input.GetAxis("Mouse X") * rotate_x_speed;
        float mouseY = Input.GetAxis("Mouse Y") * rotate_x_speed;

        Quaternion rotX = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion rotY = Quaternion.AngleAxis(-mouseY, transform.right);

        transform.RotateAround(viewpoint_t.position, Vector3.up, mouseX);//水平旋转

        //保存未旋转垂直视角前的position和rotation
        Vector3 posPre = transform.position;
        Quaternion rotPre = transform.rotation;

        transform.RotateAround(viewpoint_t.position, transform.right, -mouseY);//垂直旋转
        //记录摄像机的转动角度



        this_camera_r = transform.eulerAngles.y / 180 * Mathf.PI;
        //判断垂直角度是否符合范围
        float x = (transform.rotation).eulerAngles.x;
        //欧拉角范围为0~360，这里要转为-180~180方便判断
        if (x > 180) x -= 360;
        if (x < minAngle || x > maxAngle)//超出角度
        {
            //还原位置和旋转
            transform.position = posPre;
            transform.rotation = rotPre;

            //更新offset向量,offset与本物体同步旋转
            //我们需要通过这offset去计算本物体（包括摄像机）应该平滑移向的位置
            //如果仅仅使用RotateAround函数，当人物在移动时会出现误差
            offset = rotX * offset;
        }
        else//垂直视角符合范围的情况
        {
            //更新offset向量,offset与本物体同步旋转
            offset = rotX * rotY * offset;

            //更据角度设置摄像机位置偏移
            if (x < 0)//往上角度为负
            {
                //往上看时距离拉近
                localOffsetAngle = (x / minAngle) * localOffsetAngleUp;
            }
            else
            {
                //往下看时距离拉远
                localOffsetAngle = -(x / maxAngle) * localOffsetAngleDown;
            }
        }
        //SetLocalOffset();
    }

    /// <summary>
    /// 根据是否瞄准、垂直视角和是否有遮挡来调整摄像机与父物体的偏移
    /// </summary>
    public void SetLocalOffset()
    {
        float localOffset = 0;//摄像机与父物体（即本脚本所在的空物体）的偏移
        //根据垂直视角调整
        localOffset += localOffsetAngle;
        //根据是否瞄准而调整
        if (isAiming)
        {
            localOffset += localOffsetAim;
        }

        //根据是否有遮挡而调整
        Vector3 checkPos = transform.position + this_camera.transform.forward * localOffset;//这是没有调整前相机应该移向的位置
        for (localOffsetCollider = 0; !CheckView(checkPos); localOffsetCollider += 0.2f)//让localOffset递增直至没有遮挡
        {
            //更新checkPos为我们想要移动到的位置，再去试探
            checkPos = transform.position + this_camera.transform.forward * (localOffset + localOffsetCollider);
        }
        localOffset += localOffsetCollider;//加上这个试探出的偏移量

        Vector3 offsetPos = new Vector3(0, 0, localOffset);//这是调整后相机应该移向的位置
        //使相机平滑移动到这个位置
        this_camera.transform.localPosition = Vector3.Lerp(this_camera.transform.localPosition, offsetPos, localOffsetSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 检查玩家与摄像机之间是否有碰撞体遮挡
    /// </summary>
    /// <param name="checkPos">假设相机的位置</param>
    /// <returns></returns>
    private bool CheckView(Vector3 checkPos)
    {
        //发出射线来检测碰撞
        RaycastHit hit;
        //射线终点为玩家物体的中间位置
        Vector3 endPos = viewpoint_t.position + viewpoint_t.up * viewpoint_t.parent.GetComponent<CapsuleCollider>().height * 0.5f;

        Debug.DrawLine(checkPos, endPos, Color.blue);

        //从checkPos发射一条长度为起点到终点距离的射线
        if (Physics.Raycast(checkPos, endPos - checkPos, out hit, (endPos - checkPos).magnitude))
        {
            if (hit.transform == viewpoint_t)//如果射线打到玩家说明没有遮挡
                return true;
            else//如果射线打击到其他物体说明有遮挡
                return false;
        }
        return true;//如果射线没有打到任何物体也说明没有遮挡
    }
}