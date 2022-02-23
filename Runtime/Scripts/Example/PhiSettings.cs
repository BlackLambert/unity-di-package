using System;
using UnityEngine;

namespace SBaier.DI.Examples
{
    [Serializable]
    public class PhiSettings
    {
        [SerializeField]
        private float _num = 1.1f;
        public float Num => _num;
    }
}
