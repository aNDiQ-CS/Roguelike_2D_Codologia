using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    public void ChangeSettingsState(bool state)
    {
        _settingsPanel.SetActive(state);
    }
}
