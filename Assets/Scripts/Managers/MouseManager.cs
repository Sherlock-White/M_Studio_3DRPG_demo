using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEngine.Events;

//[System.Serializable]
//public class EventVector3 : UnityEvent<Vector3> { }

public class MouseManager : MonoBehaviour
{
    //Singleton Pattern
    public static MouseManager Instance;
    
    RaycastHit hitInfo;
    
    //public EventVector3 OnMouseClicked;
    public event Action<Vector3> OnMouseClicked;

    public Texture2D point, doorway, attack, target, arrow;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    private void Update()
    {
        SetCursorTexture();
        MouseControl();
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hitInfo))
        {
            //�л������ͼ
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;

            }
        }
    }
    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
                OnMouseClicked?.Invoke(hitInfo.point);
        }
    }
}
