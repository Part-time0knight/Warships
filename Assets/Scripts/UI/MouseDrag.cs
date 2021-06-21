using UnityEngine;
public class MouseDrag : MonoBehaviour
{
    public static MouseDrag mouseDrag { get { return _mouseDrag; } }
    private static MouseDrag _mouseDrag;
    private Camera mainCamera;
    private Vector2 mouse = Vector2.zero;
    private TowerController tower;

    public TowerController Tower { get { return tower; } }
    public Ship SelectShip { get; set; }
    private void Awake()
    {
        if (!_mouseDrag)
            _mouseDrag = this;
        else
            Destroy(gameObject);
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (tower != null)
        {
            UpdateMousePosition();
            tower.Drag(mouse);
            if (SelectShip)
            {
                Vector2Int size = tower.GetSize();
                SelectShip.EnterCell(mouse.x, mouse.y, size.x, size.y);
                
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopDrag();
            }
        }
    }
    private void UpdateMousePosition()
    {
        Vector3 mouseV3 = Input.mousePosition;
        mouseV3.z = mainCamera.nearClipPlane;
        mouseV3 = mainCamera.ScreenToWorldPoint(mouseV3);
        mouse.x = mouseV3.x;
        mouse.y = mouseV3.y;
    }
    public void StartDrag(TowerController tower)
    {
        this.tower = tower;
        Cursor.visible = false;
    }
    public void StopDrag()
    {
        if (SelectShip)
            SelectShip.TowerSet(tower);
        else
            tower.Delete();
        tower = null;
        Cursor.visible = true;
    }
    /*
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
            if (Input.GetMouseButtonUp(0))
            {
                StopDragTower();
            }
        }

    }*/
}
