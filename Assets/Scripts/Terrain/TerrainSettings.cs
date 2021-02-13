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
        public Gradient UpperGradient => upperGradient;
        public Gradient BottomGradient => bottomGradient;
        public AnimationCurve Curve => curve;
        public int UpperModifier => upperModifier;
        public int BottomModifier => bottomModifier;
        
        [SerializeField] private Vector2 offset;
        [SerializeField] private float scale;
        [SerializeField] private float minLevel;
        [SerializeField] private int resolution;
        [SerializeField] private int octaveCount;
        [SerializeField] private Gradient upperGradient;
        [SerializeField] private Gradient bottomGradient;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private int upperModifier;
        [SerializeField] private int bottomModifier;

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
