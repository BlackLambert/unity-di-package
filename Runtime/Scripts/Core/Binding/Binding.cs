using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class Binding
    {
        public Type ConcreteType;
        public InstanceCreationMode CreationMode;
        public InstanceAmountMode AmountMode;
        public Func<object> CreateInstanceFunction;
        public bool InjectionAllowed;
        public Dictionary<BindingKey, object> Arguments { get; } =
            new Dictionary<BindingKey, object>();

        public Binding(Type contractType)
        {
            ConcreteType = contractType;
            CreationMode = InstanceCreationMode.Undefined;
            AmountMode = InstanceAmountMode.PerRequest;
            CreateInstanceFunction = null;
            InjectionAllowed = true;
        }

		public override string ToString()
		{
            return $"Binding (" +
                $"Concrete: {ConcreteType} | " +
                $"CreationMode: {CreationMode} | " +
                $"CreateInstanceFunction: {CreateInstanceFunction} | " +
                $"AmountMode: {AmountMode} | " +
                $"InjectionAllowed: {InjectionAllowed})" +
                $"Arguments: {Arguments}";
		}
	}
}