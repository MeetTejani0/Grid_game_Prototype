using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IAi
{
    [SerializeField] private float _damage;
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    public bool _canMove;

    public float Damage => _damage;

    public float Speed => _speed;

    public float Health => _health;

    public bool CanMove => _canMove;


    private GridBehaviour _gridBehaviour;

    private void Start()
    {
        _gridBehaviour = FindAnyObjectByType<GridBehaviour>();
    }

    public void MoveTowardPlayer()
    {
        
    }

    void Update()
    {
        if(CanMove)
        {
            MoveTowardPlayer();
        }
    }
}
