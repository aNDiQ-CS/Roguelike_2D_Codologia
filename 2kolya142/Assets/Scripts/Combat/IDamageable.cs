using System;
using UnityEngine;

public interface IDamageable
{
    public void Attack(IDamageable target, int damage);
    public void GetDamage(int damage);
    public event Action<float> Damaged;
}
