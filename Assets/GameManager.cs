using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private bool lockedCursor = false;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LockMouse();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LockMouse();
        }
    }

    private void LockMouse()
    {
        if (!lockedCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            lockedCursor = true;
        }
        else if (lockedCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            lockedCursor = false;
        }
    }
}
