using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private TowerState tower;
    private DragTower dragTower;
    private Ship parent;
    private Vector2Int position;
    public Ship Parent
    {
        get { return parent; }
    }
    public Vector2Int Position
    {
        get { return position; }
        set
        {
            if (position == null)
                position = value;
        }
    }
    public bool Free
    {
        get
        {
            if (!tower)
                return true;
            else
                return false;
        }
    }
    private void Awake()
    {
        parent = transform.parent.GetComponent<Ship>();
    }
    private void CellSet(TowerState tower)
    {
        if (this.tower && this.tower != tower)
            this.tower.TowerDestroy();
        this.tower = tower;
        tower.TowerSet(this);
    }
    public void CellFree()
    {
        tower = null;
    }
    private void OnTriggerEnter2D(Collider2D Drag)
    {
        dragTower = Drag.GetComponent<DragTower>();
        if (dragTower)
            dragTower.OnCell();
    }
    private void OnTriggerExit2D(Collider2D Drag)
    {
        if (dragTower)
        {
            dragTower.ExitCell();
            dragTower = null;
        }
    }
    private void Update()
    {
        if (dragTower)
        {
            if (!Free)
            {
                dragTower.ReplaceTower = true;
            }
            if (dragTower.Set)
            {
                CellSet(dragTower.Tower);
            }
        }

    }
}
