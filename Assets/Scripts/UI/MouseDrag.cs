using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseDrag : MonoBehaviour
{
    private static MouseDrag _mouseDrag;
    private DragTower _tower;
    private Camera mainCamera;
    public DragTower tower { get { return _tower; } }
    public static MouseDrag mouseDrag { get { return _mouseDrag; } }
    private void Awake()
    {
        if (!_mouseDrag)
            _mouseDrag = this;
        else
            Destroy(gameObject);
        mainCamera = Camera.main;
    }
    public void StartDragTower(DragTower tower)
    {
        _tower = tower;
        Cursor.visible = false;
    }
    public void StopDragTower()
    {
        if (_tower != null)
        {
            _tower.Set = true;
            _tower = null;
        }
        Cursor.visible = true;
    }
    private void Update()
    {
        if (_tower != null)
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = mainCamera.nearClipPlane;
            mouse = mainCamera.ScreenToWorldPoint(mouse);
            mouse.x -= mouse.x % Ship.CELL_SIZE + Ship.CELL_SIZE / 2f;
            mouse.y -= mouse.y % Ship.CELL_SIZE + Ship.CELL_SIZE / 2f;
            mouse.z = 0f;
            _tower.transform.position = mouse;
            if (Input.GetMouseButtonDown(0))
            {
                StopDragTower();
            }
        }

    }
}
