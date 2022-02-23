using UnityEngine;

namespace SBaier.DI
{
    public class ToObjectBindingContext<TConcrete> : ToBindingContext<TConcrete> where TConcrete : UnityEngine.Object
    {
        public ToObjectBindingContext(BindingArguments arguments) : base(arguments) { }

        public FromBindingContext FromResources(string path)
        {
            ValidateRessource(path);
            _binding.CreationMode = InstanceCreationMode.FromResources;
            _binding.CreateInstanceFunction = () => Resources.Load<TConcrete>(path);
            return new FromBindingContext(_arguments);
        }

        private void ValidateRessource(string path)
        {
            if (Resources.Load<TConcrete>(path) == null)
                throw new MissingComponentException($"There is no ressource of type {typeof(TConcrete)} at path {path}");
        }
    }
}
