using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBuild : MonoBehaviour
{
    [SerializeField] private GameObject message;
    public void ButtonEndBuild()
    {
        Ship ship = null;
        bool end = false;
        for (int i = 0; i < Save.SHIPS_NUMBER && !end; i++)
        {
            ship = Save.GetShip(i);
            if (ship && ship.gameObject.activeInHierarchy)
                end = true;
        }
        Debug.Log(ship);
        if (end && ship && !ship.ShipIsFree())
            SceneController.sceneController.PreviousScene();
        else
            ShowMessage();

    }
    private void ShowMessage()
    {
        message.SetActive(true);
    }
}
