  using UnityEngine;
public class PlayerMgr : MonoBehaviour
{
    public static PlayerMgr Instance = null;
    //旋转参数
    private float xspeed = -3f;
    private float yspeed = 3f;
    private Vector3 rotaVector3;

    private Rigidbody this_r;
    public float speed = 2;
    private CharacterController Cc;
    public Transform main_camera;
    void Awake()
    {
        Instance = this;
        this_r = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rotaVector3 = transform.localEulerAngles;
        Cc = gameObject.GetComponent<CharacterController>();

    }
    private void Update()
    {
        //移动
        if (Input.GetKey(KeyCode.W))
        {
            var VecSpeed = main_camera.TransformPoint(Vector3.forward * speed) - transform.position;
            this_r.velocity = new Vector3(VecSpeed.x, 0, VecSpeed.z);
        }else if (Input.GetKey(KeyCode.D))
        {
            var VecSpeed = main_camera.TransformPoint(Vector3.right * speed) - transform.position;
            this_r.velocity = new Vector3(VecSpeed.x, 0, VecSpeed.z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            var VecSpeed = main_camera.TransformPoint(Vector3.back * speed) - transform.position;
            this_r.velocity = new Vector3(VecSpeed.x, 0, VecSpeed.z);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            var VecSpeed = main_camera.TransformPoint(Vector3.left * speed) - transform.position;
            this_r.velocity = new Vector3(VecSpeed.x, 0, VecSpeed.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this_r.velocity = Vector3.up * speed;
        }

        //旋转
        if (Input.GetMouseButton(1))
        {

            rotaVector3.y += Input.GetAxis("Mouse X") * yspeed;
            rotaVector3.x += Input.GetAxis("Mouse Y") * xspeed;
            main_camera.rotation = Quaternion.Euler(rotaVector3);
        }
    }  
}