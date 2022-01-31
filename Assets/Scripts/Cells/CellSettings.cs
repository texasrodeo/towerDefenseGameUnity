using UnityEngine;

namespace Field.Settings
{
    [CreateAssetMenu(menuName = "Cell Setting", order = 0)]
    public class CellSettings : ScriptableObject
    {
        public Color hoverColor;

        public Color notEnoughMoneyColor;

        public Vector3 buildOffsetPosition;
    }
}