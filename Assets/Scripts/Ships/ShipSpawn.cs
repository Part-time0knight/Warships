using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawn : MonoBehaviour
{
    public const float POS_X = -5f;
    public const float POS_Y = 0f;
    [SerializeField] Ship[] ships;
    private void Awake()
    {
        Ship ship = Save.LoadShip(Save.SelectedShip);
        if (!ship)
        {
            ship = ships[Save.SelectedShip];
            Instantiate(ship, new Vector3(POS_X, POS_Y, 0), Quaternion.identity);
        }
    }
}
