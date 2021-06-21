using UnityEngine;

public class EndBuild : MonoBehaviour
{
    [SerializeField] private GameObject message;
    public void ButtonEndBuild()
    {
        Ship ship = null;
        bool end = false;

        ship = Save.GetShip(Save.SelectedShip);
        if (ship && ship.gameObject.activeInHierarchy)
            end = true;
        if (end && ship && !ship.IsFreeSpace())
            SceneController.sceneController.PreviousScene();
        else
            ShowMessage();

    }
    private void ShowMessage()
    {
        message.SetActive(true);
    }
}
