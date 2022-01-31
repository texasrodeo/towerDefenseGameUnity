using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(menuName = "Bullet Settings", order = 3)]
    public class BulletSettings : ScriptableObject
    {
        public float speed;

        public int damage;

        public float explosionRange;
    }
}