using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] protected Vector2Int size;
    private bool set = false;
    private Vector2Int parentCellIndex;

    public Vector2Int GetSize()
    {
        return size;
    }
    public void Drag(Vector2 MousePos)
    {
        if (!set)
            transform.position = MousePos;
    }
    public void Set(GameObject cell)
    {
        set = true;
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;

    }
    public void Delete()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
