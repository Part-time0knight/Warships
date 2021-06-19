using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public const float CELL_SIZE = 0.5F;
    public int SaveIndex { get { return saveIndex; } }

    [SerializeField] protected Cell cell;
    protected int height = 0;
    protected int width = 0;
    protected bool[,] grid;
    protected int saveIndex;
    protected List<Cell> cells;
    protected abstract bool GridCheck(int posX, int posY);
    public Ship()
    {
        cells = new List<Cell>();
    }
    private void Awake()
    {
        InitShip();
        grid = new bool[width, height];
        for (int iX = 0; iX < width; iX++)
            for (int iY = 0; iY < height; iY++)
                grid[iX, iY] = GridCheck(iX, iY);
        CreateField();
    }
    private void CreateField()
    {
        for (int iX = 0; iX < width; iX++)
            for (int iY = 0; iY < height; iY++)
            {
                if (grid[iX, iY])
                {
                    Vector3 pos = Vector3.zero;
                    pos.x = (-1f * width / 2f + CELL_SIZE) * CELL_SIZE + (iX * CELL_SIZE);
                    pos.y = (-1f * height / 2f + CELL_SIZE) * CELL_SIZE + (iY * CELL_SIZE);
                    Cell item = Instantiate(cell, gameObject.transform);
                    cells.Add(item);
                    item.Position = new Vector2Int(iX, iY);
                    item.transform.localPosition = pos;
                }
            }
    }
    protected abstract void InitShip();
    public abstract void SaveShip();
    public bool ShipIsFree()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].Free)
                return true;
        }
        return false;
    }
}
