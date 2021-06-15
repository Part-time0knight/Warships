using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerState : MonoBehaviour
{
    public Vector2Int Size { get { return size; } }

    [SerializeField] private Vector2Int size = Vector2Int.one;
    [SerializeField] private int Cost = 0;
    private List<Cell> cells;
    private bool onShip = false;

    private void Awake()
    {
        cells = new List<Cell>();
    }
    public void TowerSet(Cell cell)
    {
        if (!onShip)
        {
            onShip = true;
            transform.parent = cell.transform;
            cell.Parent.SaveShip();
        }
        cells.Add(cell);
    }
    public void TowerDestroy()
    {
        for (int i = 0; i < cells.Count; i++)
            cells[i].CellFree();
        Destroy(gameObject);
    }
}
