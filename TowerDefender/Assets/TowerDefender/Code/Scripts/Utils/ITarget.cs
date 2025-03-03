using UnityEngine;

namespace Utils
{
    public interface ITarget
    {
        Vector3 Position { get; }
        float CalculateSqDistance(Vector3 from);
        void TakeDamage(int damage);
    }
}