using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu( menuName = "Enemies Settings", order = 1)]
    public class EnemiesSettings : ScriptableObject
    {
        public float hp;

        public float speed;

        public int reward;

        public int damageToPlayer;

    }
}