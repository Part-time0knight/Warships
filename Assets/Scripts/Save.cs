using System.Collections.Generic;

public static class Save
{
    private static Dictionary<int, Ship> ships;
    private static int selectedShip;
    public static int SelectedShip
    {
        get
        {
            return selectedShip;
        }
        set
        {
            selectedShip = value;
        }
    }
    static Save()
    {
        ships = new Dictionary<int, Ship>();
    }
    public static void HideShip()
    {
        if (ships.ContainsKey(selectedShip))
            ships[selectedShip].gameObject.SetActive(false);
    }
    public static void SaveShip(Ship ship, int ind)
    {
        ships.Add(ind, ship);
        selectedShip = ind;
    }
    public static Ship LoadShip(int index)
    {
        selectedShip = index;
        if (ships.ContainsKey(selectedShip))
        {
            ships[index].gameObject.SetActive(true);
            return ships[index];
        }
        return null;
    }
    public static Ship GetShip(int index)
    {
        return ships[index];
    }
}
