using UnityEngine;

namespace SBaier.DI
{
    public abstract class InstanceCreationModeValidator 
    {
        public abstract void Validate(InstantiationInfo instantiationInfo);
    }
}
