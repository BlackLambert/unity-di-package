using System;

namespace SBaier.DI
{
    public struct BindingKey
    {
        public readonly Type Type { get; }
        public readonly IComparable ID { get; }

        public BindingKey(Type contract)
        {
            Type = contract;
            ID = null;
        }

        public BindingKey(Type contract, IComparable iD)
        {
            Type = contract;
            ID = iD;
        }

        public override bool Equals(object obj)
        {
            return obj is BindingKey other && this.Equals(other);
        }

        public bool Equals(BindingKey k)
        {
            return Type == k.Type && Equals(ID, k.ID);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Type, ID);
        }
        
        public static bool operator ==(BindingKey lhs, BindingKey rhs) => lhs.Equals(rhs);

        public static bool operator !=(BindingKey lhs, BindingKey rhs) => !(lhs == rhs);

		public override string ToString()
		{
            return $"{nameof(BindingKey)}({Type} | {ID})";
		}
	}
}

