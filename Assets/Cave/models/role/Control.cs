using UnityEngine;
namespace ControlName {
    //控制器总类
    public class Control : MonoBehaviour
    {
        //方向键上
        public virtual void setUp() { 
        }
        public virtual void outUp()
        {
        }

        //Alpha1
        public virtual void setalpha1() {
        }
        //Alpha2
        public virtual void setalpha2()
        {
        }
        //Alpha3
        public virtual void setalpha3()
        {
        }

        //方向键右
        public virtual void setRight()
        {
        }
        public virtual void outRight()
        {
        }

        //方向键下
        public virtual void setBelow()
        {
        }
        public virtual void outBelow()
        {
        }

        //方向键左
        public virtual void setLeft()
        {
        }
        public virtual void outLeft()
        {
        }

        //加速键
        public virtual void setLeftShift()
        {

        }
        public virtual void outLeftShift()
        {

        }

        //空格 起跳键
        public virtual void setSpace()
        {

        }
        public virtual void outSpace()
        {

        }

        //电脑的鼠标右键 UI自定义
        public virtual void setMouseRight()
        {

        }
        public virtual void outMouseRight()
        {

        }

        //F 获取道具
        public virtual void setGetProps()
        {
        }
    }
}