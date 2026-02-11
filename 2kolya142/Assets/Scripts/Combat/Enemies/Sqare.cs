using UnityEngine;

public class Sqare : Enemy
{
    [SerializeField] private Transform[] _patrolingPoints;

    private int _patrolingIndex = 0;
    private bool _isMoving = false;
    private float _patrolingTime = 0f;    

    public override void Attack(IDamageable target, int damage)
    {
        target.GetDamage(damage);
    }

    public override void GetDamage(int damage)
    {
        Debug.Log("Кубику болльно и обидно :("); 
        base.GetDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.GetComponent<IDamageable>().GetDamage(_dmg);
        }
    }

    private void Update()
    {
        CheckState();
    }

    public override void CheckState()
    {
        var triggerCast = Physics2D.CircleCast(transform.position, _enemyData.TriggerRadius, Vector2.zero, float.PositiveInfinity, 1<<6);
        if (triggerCast)
        {
            OnPursue(triggerCast.transform);
        }
        else
        {
            OnPatroling();
        }
    }

    public override void OnPursue(Transform target)
    {
        Vector3 currentPos = transform.position;
        transform.position = Vector3.MoveTowards(currentPos, target.position, _enemyData.Speed * Time.deltaTime);
    }

    public override void OnPatroling()
    {        
        if (_isMoving)
        {
            Vector3 currentPos = transform.position;
            Vector3 nextPos = _patrolingPoints[(_patrolingIndex)].position;
            transform.position = Vector3.MoveTowards(currentPos, nextPos, _enemyData.Speed * Time.deltaTime);
            
            float distance = Vector3.Distance(currentPos, nextPos);

            if (distance < 0.001f)
            {
                _isMoving = false;
            }
        }
        else
        {
            _patrolingTime += Time.deltaTime;
            if (_patrolingTime > _enemyData.PatrolingDelay)
            {
                _isMoving = true;
                _patrolingTime = 0f;
                _patrolingIndex = (_patrolingIndex + 1) % _patrolingPoints.Length;
            }
        }
    }
}