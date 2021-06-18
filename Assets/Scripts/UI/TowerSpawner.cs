using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawner : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private DragTower tower;
    public void TowerGet()
    {
        DragTower spawn = Instantiate(tower);
        MouseDrag.mouseDrag.StartDragTower(spawn);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        TowerGet();
    }
}
