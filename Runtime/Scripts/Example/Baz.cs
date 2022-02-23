using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI.Examples
{
    public class Baz : MonoBehaviour
    {
        [SerializeField]
        private string _name = "Baz";
        public string Name => FromerBaz == null ? _name : $"{FromerBaz.Name}-{_name}";

        public Baz FromerBaz { get; set; }
	}
}