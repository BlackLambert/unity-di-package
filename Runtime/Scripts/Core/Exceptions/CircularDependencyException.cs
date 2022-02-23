using System;
using UnityEngine;

namespace SBaier.DI
{
    public class CircularDependencyException : Exception
    {
        public CircularDependencyException(BindingKey bindingKey) : base($"Cicular dependency detectend while resolving contract {bindingKey}") { }
    }
}
