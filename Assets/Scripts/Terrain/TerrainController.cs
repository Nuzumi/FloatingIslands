using System;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public class TerrainController : MonoBehaviour
    {
        public TerrainSettings settings;
        
        public NoiseGenerator generator;
        public ColorController colorController;
        public TerrainSmoothnessController smoothnessController;

        public List<TerrainObject> terrains;

        private void Awake()
        {
            settings.ClearStatistics();
            generator = new NoiseGenerator(settings);
            colorController = new ColorController(settings);
            smoothnessController = new TerrainSmoothnessController(settings);
        }

        private void Start()
        {
            terrains.ForEach(t => t.SetUp());
            terrains.ForEach(t => t.ColorVertices());
        }

        private void FixedUpdate()
        {
            settings.ClearStatistics();
            terrains.ForEach(t => t.SetUp());
            terrains.ForEach(t => t.ColorVertices());
        }
    }
}
