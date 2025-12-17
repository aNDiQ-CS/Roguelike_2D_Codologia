using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private SettingsController _settingsController;

    public void OpenSettings()
    {
        _settingsController.ChangeSettingsState(true);
    }

    public void Quit()
    {
        Debug.Log("Выходим из игры");
        Application.Quit();
    }
}
