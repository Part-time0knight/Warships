using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShipChibi : Ship
{
    public const int SAVE_INDEX = 1;
    private const int HEIGHT = 18;
    private const int WIDTH = 10;
    public ShipChibi()
    {
        height = HEIGHT;
        width = WIDTH;
        saveIndex = SAVE_INDEX;
    }
    protected override bool GridCheck(int posX, int posY)
    {
        bool truth;
        List<bool> check = new List<bool>();
        truth = posX > 3 && posX < 6;
        check.Add(truth);
        truth = posX > 2 && posX < 7 && posY > 0 && posY < 15;
        check.Add(truth);
        truth = posX > 0 && posX < 9 && posY == 4;
        check.Add(truth);
        truth = ( posX == 1 || posX == 8 ) && posY > 2 && posY < 6;
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
