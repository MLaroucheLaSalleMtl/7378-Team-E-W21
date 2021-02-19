using UnityEngine;
namespace SetActiveName
{
    //窗口显示隐藏类
    public class SetActive: MonoBehaviour
    {
        public GameObject set_active_gameobject;
        public void setActive(bool set_active_bool)
        {
            //set_active_gameobject.SetActive(set_active_bool);
            set_active_gameobject.transform.GetComponent<Animation>().Play("book exit");
            setIncident();
        }
        //子类要使用的方法
        public virtual void setIncident()
        {
        }
    }
}