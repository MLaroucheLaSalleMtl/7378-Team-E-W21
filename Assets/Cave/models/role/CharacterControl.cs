using ControlName;
using UnityEngine;
public class CharacterControl : Control
{
    //角色控制器
    //动画检测器(用于检测动画的时间)
    public AnimatorStateInfo aniInfo;

    public Transform main_camera_t;
    public Animator this_animator;
    public MainCamera main_camera_s;

    public int if_blend = 1;//这个用于判断
    public float blend = 1;//1空闲 2走路 3奔跑
    public float blend_speed = 0.05f;//转动速度
    public float move_speed;//这个速度是假的

    private bool is_up;
    private bool is_right;
    private bool is_below;
    private bool is_left;
    private bool is_left_shift;
    private bool is_mouse_right;

    //检测跑着跳动画
    public bool is_jump;

    [Header("瞄准的点")]
    public Vector3 targetPos;
    [Header("瞄准速度")]
    public float aimSpeed = 1;
    [Header("瞄准范围")]
    public float range = 1;
    [Header("瞄准的物体")]
    public Transform targetTrans;
    // Use this for initialization1
    void Start() {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //检测每一帧
        aniInfo = this_animator.GetCurrentAnimatorStateInfo(0);
        //检测动画跳aniInfo.fullPathHash == jump_int代表jump状态结束
        if (aniInfo.normalizedTime >= 0.5 && is_jump)
        {
            is_jump = false;
            this_animator.SetBool("is_jump", false);//返回Blend Tree状态
        }

        this_animator.SetFloat("Blend", blend, blend_speed, Time.deltaTime);

        //控制人物移动
        //Vector3 vel = this_script.this_rigidbody.velocity;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f || is_mouse_right)
        {
            float sr = Mathf.Sin(main_camera_s.this_camera_r);
            float cr = Mathf.Cos(main_camera_s.this_camera_r);
            //人物运动(这里使用了人物动画本身会移动得效果)
            //this_rigidbody.velocity = new Vector3((v * sr + h * cr) * move_speed, vel.y, (v * cr - h * sr) * move_speed);
            //改变人物朝向 判断是否是瞄准 按键改变朝向优先
            if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3((v * sr + h * cr), 0, (v * cr - h * sr)));
            }
            /*if (is_mouse_right)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(sr, 0, cr));
                this_animator.SetFloat("v", v, blend_speed, Time.deltaTime);
                this_animator.SetFloat("h", h, blend_speed, Time.deltaTime);
            }
            else if(Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3((v * sr + h * cr), 0, (v * cr - h * sr)));
            }
        }

        //鼠标右键干的事情
        if (is_mouse_right)
        {
            SetTarget();
        }
        */
        }
    }
    public void SetTarget()
    {
        //从摄像机位置向摄像机正方向发射射线（即从屏幕视口中心发出）
        RaycastHit hit;
        if (Physics.Raycast(main_camera_t.position,  main_camera_t.forward, out hit, range))
        {
            //若射线打到非玩家的物体则将该物体设为目标
            //我这里并没有进行判断该物体是否是玩家，因为我设置的玩家位于屏幕的偏左下位置，射线不会穿过玩家
            //需要的话，可以给玩家设定layer,然后让射线屏蔽这个layer
            targetPos = hit.point;
        }
        else
        {
            //若射线没有打到物体则将目标设为射线的终点
            targetPos =  main_camera_t.position + (main_camera_t.forward * range);
        }
        //画出射线便于观察（不会显示在game中）
        Debug.DrawRay(main_camera_t.position, main_camera_t.forward * range, Color.green);
        targetTrans.position = Vector3.Slerp(targetTrans.position, targetPos, aimSpeed);
    }

    //最后的检测方法
    private void setFinalTest(int forward_value)
    {
        //如果再次传入那么就是不起作用的请求
        if (if_blend != forward_value)
        {
            blend = forward_value;
        }
        if_blend = forward_value;
    }
    //向前检测方法
    private void setForward() {
        if (is_left_shift)
        {
            setFinalTest(3);
        }
        else
        {
            setFinalTest(2);
        }
    }
    //停止向前检测方法
    private void outForward()
    {
        if (!is_up && !is_right && !is_below && !is_left)
        {
            setFinalTest(1);
        }
    }

    public override void setUp()
    {
        is_up = true;
        
        setForward();
    }
    public override void outUp()
    {
        is_up = false;
        outForward();
    }
    public override void setRight()
    {
        is_right = true;
        setForward();
    }
    public override void outRight()
    {
        is_right = false;
        outForward();
    }
    public override void setBelow()
    {
        is_below = true;
        setForward();
    }
    public override void outBelow()
    {
        is_below = false;
        outForward();
    }
    public override void setLeft()
    {
        is_left = true;
        setForward();
    }
    public override void outLeft()
    {
        is_left = false;
        outForward();
    }

    public override void setLeftShift()
    {
        is_left_shift = true;
        if (is_up || is_left || is_below || is_right)
        {
            setFinalTest(3);
        }
    }
    public override void outLeftShift()
    {
        is_left_shift = false;
        if (is_up || is_left || is_below || is_right)
        {
            setFinalTest(2);
        }
        else
        {
            setFinalTest(1);
        }
    }

    public override void setSpace()
    {
        if (!is_jump)
        {
            //this_script.this_animator.SetBool("is_jump", true);
            is_jump = true;
        }
    }


}