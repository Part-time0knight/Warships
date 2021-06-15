using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragTower : MonoBehaviour
{
    public static readonly Color RIGHT;
    public static readonly Color WRONG;
    public static readonly Color REPLACE;

    public bool Set {
        get { return set; }
        set
        {
            if (inCollision)
                set = value;
            else
                set = false;
        } 
    }
    public bool ReplaceTower
    {
        get { return replaceTower; }
        set { replaceTower = value; }
    }
    public TowerState Tower { get { return tower; } }

    [SerializeField] private TowerState tower;
    [SerializeField] private GameObject DragCell;

    private Vector2Int Size;
    private bool set = false;
    private int cellNumber;
    private int cellCount = 0;
    private bool inCollision = false;
    private SpriteRenderer[,] sprites;
    private BoxCollider2D boxCollider;
    private bool inDeath = false;
    private bool replaceTower = false;


    static DragTower()
    {
        RIGHT = new Color(0, 179, 0);
        WRONG = new Color(139, 0, 0);
        REPLACE = new Color(230, 166, 0);
    }

    private void Awake()
    {
        Size = tower.Size;
        Vector2 multiplier = Size;
        Vector2 colliderOffset = Vector2.zero;
        float offsetX = -1f * (Size.x - 1f) / 2f * Ship.CELL_SIZE;
        float offsetY = -1f * (Size.y - 1f) / 2f * Ship.CELL_SIZE;
        float offsetModX = 0f;
        float offsetModY = 0f;
        if (Size.x % 2f == 0f)
            offsetModX = Ship.CELL_SIZE / 2f;
        if (Size.y % 2f == 0f)
            offsetModY = -Ship.CELL_SIZE / 2f;
        sprites = new SpriteRenderer[Size.x, Size.y];
        for (int ix = 0; ix < Size.x; ix++)
            for (int iy = 0; iy < Size.y; iy++)
            {
                sprites[ix, iy] = Instantiate(DragCell, transform).GetComponent<SpriteRenderer>();
                Vector3 cellPosition = new Vector3(offsetX + offsetModX + ix * Ship.CELL_SIZE, offsetY + offsetModY + iy * Ship.CELL_SIZE);
                sprites[ix, iy].transform.localPosition = cellPosition;
            }
        cellNumber = Size.x * Size.y;
        boxCollider = GetComponent<BoxCollider2D>();
        multiplier.x *= boxCollider.size.x;
        multiplier.y *= boxCollider.size.y;
        colliderOffset.x = offsetModX;
        colliderOffset.y = offsetModY;
        boxCollider.size= multiplier;
        boxCollider.offset = colliderOffset;
        tower = Instantiate(tower, transform);
        Vector3 towerPosition = new Vector3(offsetModX, offsetModY);
        tower.transform.localPosition = towerPosition;
    }

    public void TowerDestroy()
    {
        Destroy(gameObject);
    }
    public void OnCell()
    {
        cellCount++;
    }
    public void ExitCell()
    {
        cellCount--;
    }
    private void Update()
    {
        if (cellNumber == cellCount)
            inCollision = true;
        else
            inCollision = false;
        
    }
    private void LateUpdate()
    {
        if (inDeath)
            TowerDestroy();
        if (MouseDrag.mouseDrag.tower != this && !inDeath)
        {
            inDeath = true;
        }
        if (!inCollision)
        {
            for (int ix = 0; ix < Size.x; ix++)
                for (int iy = 0; iy < Size.y; iy++)
                    sprites[ix, iy].color = WRONG;
        }
        else if (replaceTower)
        {
            for (int ix = 0; ix < Size.x; ix++)
                for (int iy = 0; iy < Size.y; iy++)
                    sprites[ix, iy].color = REPLACE;
        }
        else if (sprites[0, 0].color != RIGHT)
        {
            for (int ix = 0; ix < Size.x; ix++)
                for (int iy = 0; iy < Size.y; iy++)
                    sprites[ix, iy].color = RIGHT;
        }
        replaceTower = false;
    }
}
