using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private DragTower tower;
    public void TowerGet()
    {
        DragTower spawn = Instantiate(tower);
        MouseDrag.mouseDrag.StartDragTower(spawn);
    }
}
