using System;
using UnityEngine;

namespace Utils
{
    public interface ITarget
    {
        Vector3 Position { get; }
        Action OnDeath { get; set; }

        float CalculateSqDistance(Vector3 from);
        void TakeDamage(int damage);
    }
}