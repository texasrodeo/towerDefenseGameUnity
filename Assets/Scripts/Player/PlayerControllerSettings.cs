using UnityEngine;

namespace Player
{
    [CreateAssetMenu( menuName = "Player Settings", order = 5)]
    public class PlayerControllerSettings : ScriptableObject
    {
        public int health;

        public int startBalance;
    }
}