using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{    
    [SerializeField] protected EnemyData _enemyData;
    
    protected int _hp;
    protected int _dmg;
    protected float _currentSpeed;
    protected EnemyState _state;
    public abstract void Attack(IDamageable target, int damage);

    public abstract void GetDamage(int damage);

    private void Awake()
    {
        _hp = _enemyData.Health;
        _dmg = _enemyData.Damage;
        _currentSpeed = _enemyData.Speed;
        _state = EnemyState.Idle;
    }

    public virtual void OnIdle() { }
    public virtual void OnPatroling() { }
    public virtual void OnPursue() { }
    public virtual void OnAttacking() { }
    public virtual void OnDeath() { }
}