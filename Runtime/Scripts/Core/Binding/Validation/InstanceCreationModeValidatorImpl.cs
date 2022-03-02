using System;

namespace SBaier.DI
{
    public class InstanceCreationModeValidatorImpl : InstanceCreationModeValidator
    {
        public override void Validate(InstantiationInfo instantiationInfo)
        {
            switch(instantiationInfo.CreationMode)
			{
                case InstanceCreationMode.Undefined:
                    throw new InvalidBindingException($"{instantiationInfo} has no creation mode defined." +
                        $"Please specify the creation mode.");
                case InstanceCreationMode.FromFactory:
                case InstanceCreationMode.FromInstance:
                case InstanceCreationMode.FromMethod:
                case InstanceCreationMode.FromNew:
                case InstanceCreationMode.FromPrefabInstance:
                case InstanceCreationMode.FromResourcePrefabInstance:
                case InstanceCreationMode.FromResources:
                case InstanceCreationMode.FromNewComponentOnNewGameObject:
                case InstanceCreationMode.FromNewComponentOn:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

}
