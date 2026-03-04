using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private Transform[] _patrolingPoints;

    private void Awake()
    {
        _stateMachine.Initialize(
            new IdleState(_stateMachine),
            new PatrolingState(_patrolingPoints, _stateMachine)
            );
    }
}
