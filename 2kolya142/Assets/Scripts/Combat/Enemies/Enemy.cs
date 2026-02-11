using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{    
    [SerializeField] protected EnemyData _enemyData;
    
    protected int _hp;
    protected int _dmg;
    protected float _currentSpeed;
    protected EnemyState _state;

    public event Action<float> Damaged;
    public event Action Died;

    public abstract void Attack(IDamageable target, int damage);

    public virtual void GetDamage(int damage)
    {
        _hp -= damage;
        Damaged?.Invoke(_hp / (float)_enemyData.Health);
        if (_hp <= 0)
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        _hp = _enemyData.Health;
        _dmg = _enemyData.Damage;
        _currentSpeed = _enemyData.Speed;
        _state = EnemyState.Idle;
    }
    public virtual void CheckState() { }
    public virtual void OnIdle() { }
    public virtual void OnPatroling() { }
    public virtual void OnPursue(Transform target) { }
    public virtual void OnAttacking() { }
    public virtual void OnDeath() { }
}