using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class Binding : InstantiationInfo
    {
        public Type ConcreteType { get; set; }
        public InstanceCreationMode CreationMode { get; set; }
        public InstanceAmountMode AmountMode { get; set; }
        public Func<object> ProvideInstanceFunction { get; set; }
        public bool InjectionAllowed { get; set; }
        public Dictionary<BindingKey, object> Arguments { get; } =
            new Dictionary<BindingKey, object>();

        public Binding(Type contractType)
        {
            ConcreteType = contractType;
            CreationMode = InstanceCreationMode.Undefined;
            AmountMode = InstanceAmountMode.PerRequest;
            ProvideInstanceFunction = null;
            InjectionAllowed = true;
        }

		public override string ToString()
		{
            return $"Binding (" +
                $"Concrete: {ConcreteType} | " +
                $"CreationMode: {CreationMode} | " +
                $"CreateInstanceFunction: {ProvideInstanceFunction} | " +
                $"AmountMode: {AmountMode} | " +
                $"InjectionAllowed: {InjectionAllowed})" +
                $"Arguments: {Arguments}";
		}
	}
}