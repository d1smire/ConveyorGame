using System;
using UnityEngine;

public class ToteMovement : MonoBehaviour
{
    public event Action<GameObject> destroyed;

    [SerializeField] private float scoreAmount;
    [SerializeField] private PlayerMovement player;

    private Rigidbody _rb;
    private Transform _destination { get; set; }
    private float _speed { get; set; }

    private Vector3 _direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        player = FindAnyObjectByType<PlayerMovement>();
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
            if (player != null) 
            { 
                player.AddScores(scoreAmount);
                destroyed.Invoke(this.gameObject); 
            }
            Destroy(gameObject);
        }
        if (other.CompareTag("LosePoints"))
        {
            if (player != null) 
            {
                player.AddScores(-scoreAmount);
                destroyed.Invoke(this.gameObject); 
            }
            Destroy(gameObject);
        }
    }

    public void SetValues(Transform destination, float speed) 
    {
        _destination = destination;
        _speed = speed;
    }
}
