using UnityEngine;

namespace Towers
{
    [CreateAssetMenu(menuName = "Turret Settings", order = 2)]
    public class TurretSettings : ScriptableObject
    {

        public float range;

        public int cost;

        public float attackSpeed;

        public float turnSpeed;

        public Vector3 buildingOffset;
    }
}