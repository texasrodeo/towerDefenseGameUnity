using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveSpawner
{
    [Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;

        public int enemiesCount;

        public int waveReward;
    }
}