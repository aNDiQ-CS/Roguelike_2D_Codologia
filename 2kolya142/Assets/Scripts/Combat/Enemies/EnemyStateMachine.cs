using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState _state;
    Dictionary<Type, IEnemyState> _states = new Dictionary<Type, IEnemyState>();

    public void Initialize(params IEnemyState[] states)
    {
        foreach (var state in states)
        {
            _states.Add(state.GetType(), state);
        }
        ChangeState<IdleState>();
    }

    public void ChangeState<T>() where T : IEnemyState
    {
        _state?.Exit();
        _state = _states[typeof(T)];
        _state.Enter();
    }
}

public class IdleState : IEnemyState
{
    private EnemyStateMachine _stateMachine;

    public IdleState(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.LogWarning("Idle enter");
        _stateMachine.ChangeState<PatrolingState>();
    }

    public void Exit()
    {
        Debug.LogWarning("Idle exit");
    }
}

public class PatrolingState : IEnemyState
{
    private Transform[] _patrolingPoints;
    private EnemyStateMachine _stateMachine;

    public PatrolingState(Transform[] patrolingPoints, EnemyStateMachine stateMachine)
    {
        _patrolingPoints = patrolingPoints;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.LogWarning("Patroling enter");
    }

    public void Exit()
    {
        Debug.LogWarning("Patroling exit");
    }
}

public interface IEnemyState
{
    void Enter();
    void Exit();
}

