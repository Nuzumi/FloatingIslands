using UnityEngine;

namespace Terrain
{
    [CreateAssetMenu(menuName = "TerrainSettings", fileName = "TerrainSetting")]
    public class TerrainSettings : ScriptableObject
    {
        public float Min { get; set; }
        public float Max { get; set; }
        
        public Vector2 Offset => offset;
        public float Scale => scale;
        public float MinLevel => minLevel;
        public int Resolution => resolution;
        public int OctaveCount => octaveCount;
        public Gradient Gradient => gradient;
        public AnimationCurve Curve => curve;
        public int Modifier => modifier;
        
        [SerializeField] private Vector2 offset;
        [SerializeField] private float scale;
        [SerializeField] private float minLevel;
        [SerializeField] private int resolution;
        [SerializeField] private int octaveCount;
        [SerializeField] private Gradient gradient;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private int modifier;

        public void ClearStatistics()
        {
            Min = float.MaxValue;
            Max = float.MinValue;
        }
        
        public void SetMin(float value)
        {
            if (value < Min)
                Min = value;
        }

        public void SetMax(float value)
        {
            if (value > Max)
                Max = value;
        }
    }
}
