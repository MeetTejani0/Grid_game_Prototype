using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAi
{
    public float Damage { get; }
    public float Speed { get; }
    public float Health { get; }
    bool CanMove { get; }

    void MoveTowardPlayer();
}
