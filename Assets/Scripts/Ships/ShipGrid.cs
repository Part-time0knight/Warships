using UnityEngine;

public struct ShipGrid
{
    private Vector2Int size;
    private Cell[,] cell;
    private Inequality[] inequality;
    public ShipGrid(int width, int height, Inequality[] inequality)
    {
        size = new Vector2Int(width, height);
        cell = new Cell[width, height];
        this.inequality = inequality;
        for (int iX = 0; iX < width; iX++)
        {
            for (int iY = 0; iY < height; iY++)
            {
                if (GridCheck(iX, iY))
                    cell[iX, iY] = Cell.newCell;
                else
                    cell[iX, iY] = Cell.voidCell;
            }
        }
    }
    private bool GridCheck(int posX, int posY)
    {
        bool truth = false;
        for (int i = 0; i < inequality.Length && !truth; i++)
        {
            truth = inequality[i].Upper.x >= posX && inequality[i].Lower.x <= posX
                && inequality[i].Upper.y >= posY && inequality[i].Lower.y <= posY;
        }
        return truth;
    }
    public bool CellExist(int x, int y)
    {
        return cell[x, y].active;
    }
    public bool CellFree(int x, int y)
    {
        return cell[x, y].free;
    }

    public void TowerSet(int x, int y, int width, int height)
    {
        cell[x, y].Set(true, new Vector2Int(x, y));
        for (int iX = 0; iX < width; iX++)
        {
            int cellX = x - width / 2 + iX;
            for (int iY = 0; iY < height; iY++)
            {
                int cellY = y - height / 2 + iY;
                cell[cellX, cellY].Set(false, new Vector2Int(x, y));

            }
        }
        cell[x, y].Set(true, new Vector2Int(x, y));
    }
    public Vector2Int AnchorGet(int posX, int posY)
    {
        Vector2Int anchor = cell[posX, posY].anchorPos;
        return anchor;
    }
    public void TowerUnset(int posX, int posY)
    {
        Vector2Int anchor = cell[posX, posY].anchorPos;
        if (anchor != Vector2Int.one * -1)
            for (int iX = 0; iX < size.x; iX++)
                for (int iY = 0; iY < size.y; iY++)
                {
                    if (cell[iX, iY].anchorPos == anchor)
                        cell[iX, iY].Clear();
                }
    }
}

public struct Cell
{
    public static Cell newCell{ get;}
    public static Cell voidCell { get;}
    public bool active { get; set; }
    public bool free { get; set; }
    public bool anchor { get; set; }
    public Vector2Int anchorPos { get; set; }
    static Cell()
    {
        newCell = new Cell(true, true, false, Vector2Int.one * -1);
        voidCell = new Cell(false, true, false, Vector2Int.one * -1);
    }
    public Cell(bool active, bool free, bool anchor, Vector2Int anchorPos)
    {
        this.active = active;
        this.free = free;
        this.anchor = anchor;
        this.anchorPos = anchorPos;
    }
    public void Set(bool anchor, Vector2Int anchorPos)
    {
        free = false;
        this.anchor = anchor;
        this.anchorPos = anchorPos;
    }
    public void Clear()
    {
        free = true;
        anchor = false;
        anchorPos = Vector2Int.one * -1;
    }
}