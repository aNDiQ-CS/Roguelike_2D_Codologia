using UnityEngine;

public interface IDamageable
{
    public void Attack(IDamageable target, int damage);
    public void GetDamage(int damage);
}
