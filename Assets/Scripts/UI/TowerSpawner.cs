using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSpawner : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private TowerController tower;
    public void TowerGet()
    {
        TowerController spawn = Instantiate(tower);
        MouseDrag.mouseDrag.StartDrag(spawn);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        TowerGet();
    }
}
