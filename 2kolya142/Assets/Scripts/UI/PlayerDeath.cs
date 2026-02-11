using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerCombatSystem _playerCombatSystem;
    [SerializeField] private GameObject _deathCanvas;

    private void OnEnable()
    {
        _playerCombatSystem.Died += ShowDeathCanvas;
    }

    private void ShowDeathCanvas()
    {
        _deathCanvas.SetActive(true);
        _playerCombatSystem.Died -= ShowDeathCanvas;
    }    
}
