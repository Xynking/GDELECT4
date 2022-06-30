using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MovementSystem
{
    public class BoostData : ScriptableObject, IBoostData
    {
        [Header("Don't change vertical time below .33")]
        [SerializeField]
        private float _time;
        [SerializeField]
        private float _distance;

        public float time => _time;

        public float distance => _distance;
    }
}

