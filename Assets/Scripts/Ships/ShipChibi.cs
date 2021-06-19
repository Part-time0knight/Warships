using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShipChibi : Ship
{
    [SerializeField] private int _saveIndex;
    [SerializeField] private int Height = 18;
    [SerializeField] private int Width = 10;
    [Header("¬едите логические неравенства, описывающие поле корабл€(Xupper - х должен быть не больше этого значени€ и тд.):")]
    [SerializeField]
    private Inequality[] inequality;
    protected override void InitShip()
    {
        height = Height;
        width = Width;
        saveIndex = _saveIndex;
    }
    protected override bool GridCheck(int posX, int posY)
    {
         bool truth;
        List<bool> check = new List<bool>();
        for (int i = 0; i < inequality.Length; i++)
        {
            truth = inequality[i].xUpper >= posX && inequality[i].xLower <= posX
                && inequality[i].yUpper >= posY && inequality[i].yLower <= posY;
            check.Add(truth);
        }
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
