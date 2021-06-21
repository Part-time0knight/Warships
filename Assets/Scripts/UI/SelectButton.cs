using UnityEngine;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private int number;
    public void SelectShip()
    {
        Save.SelectedShip = number;
    }
}
