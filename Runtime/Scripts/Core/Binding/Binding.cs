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

        public Binding(Type concreteType)
        {
            ConcreteType = concreteType;
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
        
        protected bool Equals(Binding other)
        {
            return ConcreteType == other.ConcreteType && 
                   CreationMode == other.CreationMode && 
                   AmountMode == other.AmountMode && 
                   Equals(ProvideInstanceFunction, other.ProvideInstanceFunction) && 
                   InjectionAllowed == other.InjectionAllowed && 
                   Equals(Arguments, other.Arguments);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Binding)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ConcreteType != null ? ConcreteType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)CreationMode;
                hashCode = (hashCode * 397) ^ (int)AmountMode;
                hashCode = (hashCode * 397) ^ (ProvideInstanceFunction != null ? ProvideInstanceFunction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ InjectionAllowed.GetHashCode();
                hashCode = (hashCode * 397) ^ (Arguments != null ? Arguments.GetHashCode() : 0);
                return hashCode;
            }
        }
	}
}