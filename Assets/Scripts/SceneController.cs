using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _sceneController;
    public static SceneController sceneController
    {
        get
        {
            return _sceneController;
        }
    }
    private void Awake()
    {
        if (!_sceneController)
            _sceneController = this;
        else
            Destroy(gameObject);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Save.HideShip();
    }
    public void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Save.HideShip();
    }
}
