using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField][Min(0)] private int _health;    
    [SerializeField][Min(0)] private int _armor;
    [SerializeField][Min(0)] private float _speed;

    [Header("Attack")]
    [SerializeField][Min(0)] private int _damage;
    [SerializeField][Min(0)] private float _attackRange;
    [SerializeField][Min(0)] private float _attackDelay;
    [Tooltip("The time during which a player can take damage")]
    [SerializeField][Min(0)] private float _attackTime;
    [SerializeField] private EnemyState _state;

    public string Name => _name;
    public int Health => _health;
    public int Armor => _armor;
    public float Speed => _speed;
    public float AttackRange => _attackRange;
    public float AttackDelay => _attackDelay;
    public float AttackTime => _attackTime;
    public int Damage => _damage;
    public EnemyState State => _state;
}

public enum EnemyState
{
    Idle, Patroling, Aggresive, Attacking, Dead
}