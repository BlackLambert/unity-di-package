using System;

namespace SBaier.DI
{
    public class FromBindingValidatorImpl : FromBindingValidator
    {
        public override void Validate(Binding binding)
        {
            switch(binding.CreationMode)
			{
                case InstanceCreationMode.Undefined:
                    throw new InvalidBindingException($"{binding} has no creation mode defined." +
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
