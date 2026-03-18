using System;
using UnityEngine;

public class ToteMovement : MonoBehaviour
{
    public event Action<ToteMovement> OnReachedDestination;
    public event Action<float,Vector3> OnToteProcessed;

    [SerializeField] private float scoreAmount;

    private Rigidbody _rb;
    private Transform _destination { get; set; }
    private float _speed { get; set; }

    private Vector3 _direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_destination == null) return;

        Vector3 toTarget = _destination.position - transform.position;

        _direction = toTarget.normalized;
        _rb.MovePosition(_rb.position + _direction * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnToteProcessed?.Invoke(scoreAmount, transform.position);
            OnReachedDestination?.Invoke(this);
        }
        if (other.CompareTag("LosePoints"))
        {
            OnToteProcessed?.Invoke(-scoreAmount, transform.position);
            OnReachedDestination?.Invoke(this);
        }
    }

    public void SetValues(Transform destination, float speed) 
    {
        _destination = destination;
        _speed = speed;
    }
}
