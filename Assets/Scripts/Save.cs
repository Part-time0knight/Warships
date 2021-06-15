using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Save
{
    public const int SHIPS_NUMBER = 2;
    private static Ship[] ships;
    private static int selectedShip;
    public static int SelectedShip
    {
        get
        {
            return selectedShip;
        }
        set
        {
            if (value < SHIPS_NUMBER)
                selectedShip = value;
        }
    }
    static Save()
    {
        ships = new Ship[SHIPS_NUMBER];
    }
    public static void HideShip()
    {
        for (int i = 0; i < SHIPS_NUMBER; i++)
            if (ships[i])
            {
                ships[i].gameObject.SetActive(false);
            }
    }
    public static void SaveShip(Ship ship)
    {
        ships[ship.SaveIndex] = ship;
        Object.DontDestroyOnLoad(ship);
    }
    public static Ship LoadShip(int index)
    {
        if (ships[index])
            ships[index].gameObject.SetActive(true);
        return ships[index];
    }
    public static Ship GetShip(int index)
    {
        return ships[index];
    }
}
