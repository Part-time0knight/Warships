using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private int number;
    public void SelectShip()
    {
        Save.SelectedShip = number;
    }
}
