using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShipBlade : Ship
{
    public const int SAVE_INDEX = 0;
    private const int HEIGHT = 18;
    private const int WIDTH = 10;
    public ShipBlade()
    {
        height = HEIGHT;
        width = WIDTH;
        saveIndex = SAVE_INDEX;
    }
    protected override bool GridCheck(int posX, int posY)
    {
        bool truth;
        List<bool> check = new List<bool>();
        truth = posX > 3 && posX < 6 && posY > 1;
        check.Add(truth);
        truth = posX > 2 && posX < 7 && posY > 8 && posY < 13;
        check.Add(truth);
        truth = posX >= 0 && posX < 3 && posY > 1 && posY < 8 && !(posX == 0 && posY == 7);
        check.Add(truth);
        truth = posX < 10 && posX > 6 && posY > 1 && posY < 8 && !(posX == 9 && posY == 7);
        check.Add(truth);
        for (int i = 0; i < check.Count; i++)
            if (check[i])
                return true;
        return false;
    }
    public override void SaveShip()
    {
        Save.SaveShip(this);
    }
}
