using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField][Range(0.1f, 10)] float _speed;
    [SerializeField] private float _zOffset = -10f;

    void Start()
    {
        transform.position = _target.position + new Vector3(0, 0, _zOffset);
    }

    void LateUpdate()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = _target.position;
        Vector3 newPosition = Vector3.Lerp(startPos, endPos, _speed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, _zOffset);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Hit trigger");
    }
}
