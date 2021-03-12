using UnityEngine;
using ControlName;
public class KeyboardControl : MonoBehaviour {
    //游戏对象控制器 
    public CharacterControl this_characterContro;
    // Use this for initialization
    void Start () {
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           this_characterContro.setalpha1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
           this_characterContro.setalpha2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
           this_characterContro.setalpha3();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
           this_characterContro.setUp();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
           this_characterContro.outUp();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
           this_characterContro.setRight();
        }
        else if (Input.GetKeyUp(KeyCode.D)) {
           this_characterContro.outRight();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
           this_characterContro.setBelow();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
           this_characterContro.outBelow();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
           this_characterContro.setLeft();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
           this_characterContro.outLeft();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
           this_characterContro.setGetProps();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           this_characterContro.setLeftShift();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
           this_characterContro.outLeftShift();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           this_characterContro.setSpace();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
           this_characterContro.outSpace();
        }
        if (Input.GetMouseButtonDown(1))
        {
           this_characterContro.setMouseRight();
        }
        else if(Input.GetMouseButtonUp(1)) {
           this_characterContro.outMouseRight();
        }
    }
}