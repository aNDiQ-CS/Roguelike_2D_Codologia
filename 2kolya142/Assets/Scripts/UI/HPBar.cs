using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HPBar : MonoBehaviour
{
    [SerializeReference] private GameObject _damageableObject;

    private Image _image;
    private IDamageable _damageable;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        if (_damageableObject.TryGetComponent(out IDamageable damageable))
        {
            _damageable = damageable;
        }
    }

    private void Start()
    {
        _damageable.Damaged += ChangeHPBar;
    }

    private void ChangeHPBar(float percent)
    {
        Debug.LogWarning(percent);
        _image.fillAmount = percent;
    }
}
