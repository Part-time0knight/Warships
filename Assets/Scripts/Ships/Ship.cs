using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public const float CELL_SIZE = 0.5F;
    public static readonly Color FREE = new Color(1f, 1f, 1f, 0.5f);
    public static readonly Color RIGHT = new Color(0.24f, 0.52f, 0f);
    public static readonly Color WRONG = new Color(0.55f, 0f, 0f);
    public static readonly Color REPLACE = new Color(0.9f, 0.65f, 0f);
    public int SaveIndex { get { return saveIndex; } }
    [SerializeField] private int saveIndex = 0;
    [SerializeField] private SpriteRenderer cellPrefab;
    [SerializeField] private int height = 0;
    [SerializeField] private int width = 0;
    [Header("¬едите логические неравенства, описывающие поле корабл€\n(upper - вверхн€€ граница включительно, lower - нижн€€ граница включительно.):")]
    [SerializeField]
    private Inequality[] inequality;
    private SpriteRenderer[,] cellSprite;
    private ShipGrid grid;
    private BoxCollider2D ShipCollider;
    private float middleX;
    private float middleY;
    private List<TowerController> tower;
    private Vector2Int enterCell;
    private List<Vector2Int> replaceCell;
    private bool canSet = false;
    private bool replace = false;
    private void Awake()
    {
        cellSprite = new SpriteRenderer[width, height];
        grid = new ShipGrid(width, height, inequality);
        tower = new List<TowerController>();
        replaceCell = new List<Vector2Int>();
        ShipCollider = GetComponent<BoxCollider2D>();
        ShipCollider.size = new Vector2(width, height) * CELL_SIZE;
        middleX = (-1f * width / 2f + CELL_SIZE) * CELL_SIZE;
        middleY = (-1f * height / 2f + CELL_SIZE) * CELL_SIZE;
        CreateField();
        Save.SaveShip(this, saveIndex);
        Object.DontDestroyOnLoad(gameObject);
    }
    private void CreateField()
    {
        for (int iX = 0; iX < width; iX++)
            for (int iY = 0; iY < height; iY++)
            {
                if (grid.CellExist(iX, iY))
                {
                    Vector3 pos = Vector3.zero;
                    pos = ToPoint(iX, iY);
                    SpriteRenderer item = Instantiate(cellPrefab, gameObject.transform);
                    cellSprite[iX, iY] = item;
                    item.transform.localPosition = pos;
                    item.color = FREE;
                }
                else
                    cellSprite[iX, iY] = null;
            }
    }
    public bool IsFreeSpace()
    {
        for (int iX = 0; iX < width; iX++)
            for (int iY = 0; iY < height; iY++)
                if (grid.CellExist(iX, iY) && grid.CellFree(iX, iY))
                    return true;
        return false;
    }
    public void TowerSet(TowerController tower)
    {
        if (canSet)
        {
            if(replace)
            {
                while (replaceCell.Count > 0)
                {
                    deleteTower(replaceCell[replaceCell.Count - 1]);
                    replaceCell.RemoveAt(replaceCell.Count - 1);
                }
            }
            tower.Set(cellSprite[enterCell.x, enterCell.y].gameObject);
            Vector2Int towerSize = tower.GetSize();
            this.tower.Add(tower);
            grid.TowerSet(enterCell.x, enterCell.y, towerSize.x, towerSize.y);
        }
        else
            tower.Delete();
    }
    private Vector2 ToPoint(int iX, int iY)
    {
        Vector2 result = new Vector2();
        result.x = middleX + (iX * CELL_SIZE);
        result.y = middleY + (iY * CELL_SIZE);
        return result;
    }
    private Vector2Int ToIndex(float posX, float posY)
    {
        Vector2Int result = new Vector2Int();
        result.x = Mathf.RoundToInt((posX - middleX) / CELL_SIZE);
        result.y = Mathf.RoundToInt((posY - middleY) / CELL_SIZE);
        return result;
    }
    public void EnterCell(float posX, float posY, int width, int height)
    {
        //локализаци€ координат
        float localPosX = posX - transform.position.x;
        float localPosY = posY - transform.position.y;
        //------
        bool outOfRange = false;
        int rangeX = width / 2;
        int rangeY = height / 2;
        List<SpriteRenderer> selectCell = new List<SpriteRenderer>();
        Color cellColor = RIGHT;
        canSet = true;
        replace = false;
        replaceCell.Clear();

        enterCell = ToIndex(localPosX, localPosY);

        if (rangeX + enterCell.x >= this.width || enterCell.x - rangeX < 0)
            canSet = false;
        
        for (int iX = 0; iX < width && !outOfRange; iX++)//определение €чеек и нового цвета дл€ них
        {
            int cellX = enterCell.x - rangeX + iX;
            if (cellX >= this.width)
                outOfRange = true;
            else if (cellX < 0)
            {
                iX -= cellX;
                cellX = enterCell.x - rangeX + iX;
                canSet = false;
            }
            for (int iY = 0; iY < height && !outOfRange; iY++)
            {
                int cellY = enterCell.y - rangeY + iY;
                if (cellY >= this.height)
                    outOfRange = true;
                else if (cellY < 0)
                {
                    iY -= cellY;
                    cellY = enterCell.y - rangeY + iY;
                    canSet = false;
                }
                if (!outOfRange && grid.CellExist(cellX, cellY))
                {
                    selectCell.Add(cellSprite[cellX, cellY]);
                    if (!grid.CellFree(cellX, cellY) && canSet)
                    {
                        replace = true;
                        replaceCell.Add(new Vector2Int(cellX, cellY));
                    }
                }
                else
                {
                    canSet = false;
                }
            }
        }
        if (canSet && replace)
            cellColor = REPLACE;
        else if (!canSet)
            cellColor = WRONG;
        for (int i = 0; i < selectCell.Count; i++)//изменение цвета
        {
            selectCell[i].color = cellColor;
        }
        StartCoroutine(ClearCells());
    }
    private IEnumerator ClearCells()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        for (int iX = 0; iX < width; iX++)
            for (int iY = 0; iY < height; iY++)
            {
                if (grid.CellExist(iX, iY))
                    if (grid.CellFree(iX, iY) && cellSprite[iX, iY].color != FREE)
                        cellSprite[iX, iY].color = FREE;
                    else if (!grid.CellFree(iX, iY) && cellSprite[iX, iY].color != RIGHT)
                        cellSprite[iX, iY].color = RIGHT;
            }
    }
    private void deleteTower( Vector2Int towerPos )
    {
        Vector2Int anchor = grid.AnchorGet(towerPos.x, towerPos.y);
        grid.TowerUnset(towerPos.x, towerPos.y);
        if (anchor != Vector2Int.one * -1)
        {
            Debug.Log(anchor);
            Transform cellAnchor = cellSprite[anchor.x, anchor.y].transform;
            TowerController item = null;
            for (int i = 0; i < tower.Count && !item; i++)
            {
                if (tower[i].transform.parent == cellAnchor)
                {
                    item = tower[i];
                    tower.RemoveAt(i);
                }
            }
            item.Delete();
        }
    }    
    private void OnMouseEnter()
    {
        MouseDrag.mouseDrag.SelectShip = this;
    }
    private void OnMouseExit()
    {
        if (MouseDrag.mouseDrag.SelectShip == this)
            MouseDrag.mouseDrag.SelectShip = null;
    }
}
