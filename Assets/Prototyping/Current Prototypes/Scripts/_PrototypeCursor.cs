using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class _PrototypeCursor : MonoBehaviour// , IPointerDownHandler, IPointerUpHandler
{
    public Texture2D defaultCursor;
    public Texture2D clickCursor;

    public Vector2 cursorPosition = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    
    private static _PrototypeCursor _instance;
    public static _PrototypeCursor Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    
    void Start()
    {
        Cursor.SetCursor(defaultCursor, cursorPosition, cursorMode);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(clickCursor, cursorPosition, cursorMode);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(defaultCursor, cursorPosition, cursorMode);
        }
    }
}
